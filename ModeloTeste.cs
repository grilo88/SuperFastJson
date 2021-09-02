using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFastJson
{
    public class ModeloTeste
    {
        public int requestId { get; set; }
        public int[] body { get; set; }
        public Dictionary<string, object> @params { get; set; }
        public Dictionary<string, object> teste { get; set; }
    }
}
