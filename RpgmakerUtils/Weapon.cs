using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgmakerUtils
{
    public record Weapon
    {
        public int id { get; set; }
        public int animationId { get; set; }
        public string description { get; set; }
        public int etypeId { get; set; }
        public Trait[] traits { get; set; }
        public int iconIndex { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        [JsonProperty("params")]
        public int[] _params { get; set; }
        public int price { get; set; }
        public int wtypeId { get; set; }
    }

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
