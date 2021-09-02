using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperFastJson
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnBenchmark_Click(object sender, EventArgs e)
        {
            int tick = Environment.TickCount;
            int Vezes = 0;

            SuperFastJson sfj = new SuperFastJson();

            while (Environment.TickCount - tick < 1000) // 1 segundo
            {
                ReadOnlySpan<char> json =
                @"{
                    ""requestId"": 123,
                    ""body"": [ 123456, 78910, 111213, 141516 ],
                    ""params"": {
                        ""ObjetoA"": false,
                        ""ObjetoB"": false,
                        ""ObjetoC"": ""exemplo"",
                        ""ObjetoD"": false,
                        ""ObjetoE"": false,
                        ""ObjetoF"": 9856488,
                        ""ObjetoG"": false,
                        ""ObjetoH"": false,
                        ""ObjetoI"": null
                    },
                    ""teste"": {
                        ""chave"":""valor""
                    }
                }";

                var (obj, pos) = SuperFastJson.GetValueScan(json, "requestId", ":", ",");
                (obj, pos) = SuperFastJson.GetValueScan(json, "body", "[", "]", pos);
                (obj, pos) = SuperFastJson.GetValueScan(json, "ObjetoA", ":", ",", pos);
                (obj, pos) = SuperFastJson.GetValueScan(json, "ObjetoC", ":", ",", pos);
                (obj, pos) = SuperFastJson.GetValueScan(json, "ObjetoF", ":", ",", pos);
                (obj, pos) = SuperFastJson.GetValueScan(json, "ObjetoI", ":", "}", pos);
                (obj, pos) = SuperFastJson.GetValueScan(json, "teste", "{", "}", pos);

                Vezes++;
            }

            int tempoGasto = Environment.TickCount - tick;

            MessageBox.Show(this, Vezes + " deserializações em " + tempoGasto + "ms");
        }

        private void BtnBenchmark2_Click(object sender, EventArgs e)
        {
            int tick = Environment.TickCount;
            int Vezes = 0;

            while (Environment.TickCount - tick < 1000) // 1 segundo
            {
                ReadOnlySpan<char> json =
                @"{
                    ""requestId"": 123,
                    ""body"": [ 123456, 78910, 111213, 141516 ],
                    ""params"": {
                        ""ObjetoA"": false,
                        ""ObjetoB"": false,
                        ""ObjetoC"": ""exemplo"",
                        ""ObjetoD"": false,
                        ""ObjetoE"": false,
                        ""ObjetoF"": 9856488,
                        ""ObjetoG"": false,
                        ""ObjetoH"": false,
                        ""ObjetoI"": null
                    },
                    ""teste"": {
                        ""chave"":""valor""
                    }
                }";

                var teste = SuperFastJson.Deserialize<ModeloTeste>(json);

                Vezes++;
            }

            int tempoGasto = Environment.TickCount - tick;

            MessageBox.Show(this, Vezes + " deserializações em " + tempoGasto + "ms");
        }
    }
}
