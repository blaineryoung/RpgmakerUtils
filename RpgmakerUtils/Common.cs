using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgmakerUtils
{
    public class Trait
    {
        public int code { get; set; }
        public int dataId { get; set; }
        public float value { get; set; }
    }

    public class Material
    {
        public string name { get; set; }
        public int level { get; set; }
        public int[] types { get; set; }
    }
}
