using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SuperFastJson
{
    public enum SFPerformanceStrategy : byte
    {
        ContinuousSequence = 1,
        RepetitiveSequence = 2,
        DetectTokenFirst = 3,
        Normal = 4
    }

    public class SuperFastJson
    {
        public object Serialize<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public object Serialize(object obj)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(ReadOnlySpan<char> json)
        {
            throw new NotImplementedException();
        }

        public static T Deserialize<T>(ReadOnlySpan<char> json)
        {
            var instance = Activator.CreateInstance(typeof(T));
            Type type = instance.GetType();
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            int cursor = 0;
            for (int i = 0; i < props.Length; i++)
            {
                string key = props[i].Name;
                Type vType = props[i].PropertyType;

                object value;
                (value, cursor) = GetValueScan(json, key, "\"", "\"", cursor, vType);

                if (value != null)
                    props[i].SetValue(instance, value);
            }

            return (T)instance;
        }

        public static (object, int) GetValueScan(
            ReadOnlySpan<char> json, string nextKey, string tokenA, string tokenB,
            int continueIndex = 0, Type type = null)
        {
            int start = GetPosRight(json, $"\"{nextKey}\"", continueIndex);
            var (left, right) = GetPosInnerBetween(json, tokenA, tokenB, start);

            if (tokenA == "{")
            {
                return (GetObjectValues(json[left..right], type), right);
            }
            else if (tokenA == "[")
            {
                return (GetArrayValues(json[left..right], type), right);
            }
            else
                return (ValueToType(json[left..right], type), right);
        }

        static object GetObjectValues(ReadOnlySpan<char> json, Type type)
        {
            // TODO: Criar objetos recursivamente conforme a documentação json

            return null;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                Type keyType = type.GetGenericArguments()[0];
                Type valueType = type.GetGenericArguments()[1];

                Type dictType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
                object value = Activator.CreateInstance(dictType);
            }

            //var pos = GetPosRight(json,  ":");

            return new object() { };
        }

        static List<object> GetArrayValues(ReadOnlySpan<char> json, Type type)
        {
            json = json.TrimStart().TrimEnd();

            List<object> list = new();

            StringBuilder value = new();
            for (int i = 0; i < json.Length; i++)
            {
                if (json[i] == ',')
                {
                    string valor = value.ToString();
                    list.Add(valor);
                    value.Clear();
                }
                else
                    value.Append(json[i]);
            }

            return list;
        }

        private static object ValueToType(
            ReadOnlySpan<char> value, Type type)
        {
            value = value.TrimStart().TrimEnd();

            if (type is null)
            {
                if (value[0] == '"' && value[^1] == '"')
                {
                    return new string(value[1..^1]);
                }
                else if (value.SequenceEqual("true"))
                    return true;
                else if (value.SequenceEqual("false"))
                    return false;
                else if (value.SequenceEqual("null"))
                    return null;
                else
                    return ToInt(value);
            }
            else
            {
                if (type == typeof(string))
                {
                    return new string(value[1..^1]);
                }
                else if (type == typeof(bool))
                {
                    if (value.SequenceEqual("true"))
                        return true;
                    else if (value.SequenceEqual("false"))
                        return false;
                    else
                        throw new Exception("Valor inválido para o tipo " + type.Name);
                }
                if (type == typeof(int))
                {
                    return ToInt(value);
                }
                else if (value.SequenceEqual("null"))
                    return null;
                else
                    throw new NotImplementedException(type.Name);
            }
        }

        static decimal ToDecimal(ReadOnlySpan<char> c)
        {
            return 0.0M;
        }

        static int ToInt(ReadOnlySpan<char> c)
        {
            int v = 0;
            int mult = 1;
            for (int i = c.Length - 1; i > -1; i--)
            {
                var alg = c[i] - '0';
                v += mult * alg;
                mult *= 10;
            }

            return v;
        }

        private static (int, int) GetPosInnerBetween(
            ReadOnlySpan<char> text, string findA, string findB, int startIndex = 0)
        {
            int start;
            int end = GetPosRight(text, findB, start = GetPosRight(text, findA, startIndex)) - findB.Length;

            return (start, end);
        }

        #region Algoritmo 1 - Rápido
        private static int GetPosRight(
            ReadOnlySpan<char> text, ReadOnlySpan<char> findNext, int startIndex = 0)
        {
            // TODO: Faça o scanner com escape de caracteres conforme a documentação json

            int idx;
            if ((idx = text[startIndex..].IndexOf(findNext, StringComparison.Ordinal)) > -1)
            {
                idx += startIndex + findNext.Length;
            }

            return idx;
        }
        #endregion

        #region Algoritmo 2 - Lento
        //private static int GetPosRight(
        //    ReadOnlySpan<char> text, string[] findNext, int startIndex = 0)
        //{
        //    int idx, length;
        //    if (((idx, length) = IndexOfAny(text[startIndex..], findNext)).idx > -1)
        //    {
        //        idx += startIndex + length;
        //    }

        //    return idx;
        //}

        //private static (int pos, int length) IndexOfAny(ReadOnlySpan<char> text, string[] findAny)
        //{
        //    int pos = -1;

        //    // TODO: Faça o scanner com escape de caracteres conforme a documentação json

        //    for (int i = 0; i < text.Length; i++)
        //    {
        //        for (int b = 0; b < findAny.Length; b++)
        //        {
        //            if (text[0..i].EndsWith(findAny[b]))
        //            {
        //                return (i - findAny[b].Length, findAny[b].Length);
        //            }
        //        }
        //    }

        //    return (pos, 0);
        //}
        #endregion

        #region Algoritmo 3 - Lento
        //private static (int pos, int length) IndexOfAny(ReadOnlySpan<char> text, string[] findAny)
        //{
        //    int pos = -1;

        //    // TODO: Faça o scanner com escape de caracteres conforme a documentação json

        //    for (int i = 0; i < text.Length; i++)
        //    {
        //        for (int b = 0; b < findAny.Length; b++)
        //        {
        //            if (EndsWith(text[0..i], findAny[b]))
        //            {
        //                return (i - findAny[b].Length, findAny[b].Length);
        //            }
        //        }
        //    }

        //    return (pos, 0);
        //}

        //private static bool EndsWith(ReadOnlySpan<char> text, ReadOnlySpan<char> value)
        //{
        //    for (int i = text.Length - 1; i > -1; i--)
        //    {
        //        if (text[i..text.Length].SequenceEqual(value))
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}
        #endregion
    }
}
