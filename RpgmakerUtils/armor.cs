using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgmakerUtils
{
    public class ArmorGenerator
    {
        static Trait[] defaultTraits =
        {
            new Trait(){ code = 22, dataId = 1, value=0},
        };

        static Armor[] prototypes =
        {
            new Armor() { description = "Armor", name = "Armor", _params = new int[] {5,0,0,10,0,0,0,0}, etypeId=4, atypeId=4, iconIndex=1175, price=200},
            new Armor() { description = "Helmet", name = "Helmet", _params = new int[] {0,0,1,8,0,0,0,0}, etypeId=3, atypeId=4, iconIndex=1080, price=150},
            new Armor() { description = "Shield", name = "Shield", _params = new int[] {0,0,0,5,0,0,0,0}, etypeId=2, atypeId=6, iconIndex=1061, price=150},
            new Armor() { description = "Jerkin", name = "Jerkin", _params = new int[] {0,0,1,7,0,0,0,0}, etypeId=4, atypeId=3, iconIndex=1178, price=200},
            new Armor() { description = "Cap", name = "Cap", _params = new int[] {0,0,1,5,0,0,0,0}, etypeId=3, atypeId=3, iconIndex=1180, price=150},
            new Armor() { description = "Robe", name = "Robe", _params = new int[] {0,2,0,4,2,0,0,0}, etypeId=4, atypeId=2, iconIndex=1198, price=200},
            new Armor() { description = "Pointy Hat", name = "Pointy Hat", _params = new int[] {0,0,0,2,2,2,0,0}, etypeId=3, atypeId=2, iconIndex=1131, price=150},
        };

        static Material[] materials =
        {
            new Material(){ name = "Rusty", level = 1, types = new int[] {4}},
            new Material(){ name = "Iron", level = 4, types = new int[] {4}},
            new Material(){ name = "Steel", level = 8, types = new int[] {4}},
            new Material(){ name = "Hardened", level = 12, types = new int[] {4}},
            new Material(){ name = "Darksteel", level = 16, types = new int[]  {4}},
            new Material(){ name = "Mythril", level = 20, types = new int[] {4}},
            new Material(){ name = "Gnarled", level = 1, types = new int[] { 6}},
            new Material(){ name = "Birch", level = 4, types = new int[]{ 6}},
            new Material(){ name = "Cedar", level = 8, types = new int[] { 6}},
            new Material(){ name = "Oak", level = 12, types = new int[] { 6 } },
            new Material(){ name = "Heartwood", level = 16, types = new int[] { 6} },
            new Material(){ name = "Ebony", level = 20, types = new int[] { 6} },
            new Material(){ name = "Moldy", level = 1, types = new int[] {3, 2}},
            new Material(){ name = "Leather", level = 4, types = new int[] {3}},
            new Material(){ name = "Tanned Leather", level = 8, types = new int[] {3}},
            new Material(){ name = "Tempered Leather", level = 12, types = new int[] {3}},
            new Material(){ name = "Dragon Leather", level = 16, types = new int[]  {3}},
            new Material(){ name = "Pheonix Leather", level = 20, types = new int[] {3}},
            new Material(){ name = "Cotton", level = 4, types = new int[] {2}},
            new Material(){ name = "Linen", level = 8, types = new int[] {2}},
            new Material(){ name = "Silk", level = 12, types = new int[] {2}},
            new Material(){ name = "Satin", level = 16, types = new int[]  {2}},
            new Material(){ name = "Spellweave", level = 20, types = new int[] {2}},
        };
        public static IEnumerable<Armor> GenerateArmor()
        {
            List<Armor?> armors = new List<Armor?>();
            armors.Add(null);

            foreach (Material m in materials)
            {
                foreach (int i in m.types)
                {
                    foreach (Armor prototype in prototypes.Where(x => x.atypeId == i))
                    {
                        armors.Add(prototype with
                        {
                            id = armors.Count,
                            name = $"{m.name} {prototype.name}",
                            price = prototype.price * m.level,
                            _params = prototype._params.Select(x => x * m.level).ToArray()
                        });
                    }
                }
            }

            for (int i = armors.Count; i < 100; i++)
            {
                armors.Add(new Armor() { id = i, name = "Blank" });
            }

            string customArmors = File.ReadAllText("customarmors.json");
            IEnumerable<Armor> cw = JsonConvert.DeserializeObject<IEnumerable<Armor>>(customArmors);
            foreach (Armor w in cw)
            {
                armors.Add(w with { id = armors.Count });
            }

            return armors;
        }
    }


    public record Armor
    {
        public int id { get; set; }
        public int atypeId { get; set; }
        public string description { get; set; }
        public int etypeId { get; set; }
        public Trait[] traits { get; set; }
        public int iconIndex { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        [JsonProperty("params")]
        public int[] _params { get; set; }
        public int price { get; set; }
    }

}
