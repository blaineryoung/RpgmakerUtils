using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgmakerUtils
{
    public class WeaponGenerator
    {
        static Trait[] defaultTraits =
{
            new Trait(){ code = 31, dataId = 1, value=0},
            new Trait(){ code = 22, dataId = 0, value=0},
        };

        // Params = maxhp, maxmp, attack, defense, m.attack, m.defense, agility, luck
        static Weapon[] prototypes =
        {
            new Weapon(){ description = "A sword", iconIndex= 906, name = "Sword", price = 200, _params = new int[] { 0, 0, 3, 2, 0, 0, 0, 0 },  wtypeId = 2, traits = defaultTraits, etypeId = 1, animationId = 6, },
            new Weapon(){ description = "A dagger", iconIndex= 896, name = "Dagger", price = 150, _params = new int[] { 0, 0, 2, 2, 0, 0, 1, 0 },  wtypeId = 1, traits = defaultTraits, etypeId = 1, animationId = 6, },
            new Weapon(){ description = "A axe", iconIndex= 931, name = "Axe", price = 200, _params = new int[] { 0, 0, 5, 0, 0, 0, 0, 0 },  wtypeId = 4, traits = defaultTraits, etypeId = 1, animationId = 6, },
            new Weapon(){ description = "A gun", iconIndex= 990, name = "Gun", price = 400, _params = new int[] { 0, 0, 3, 0, 0, 0, 0, 0 },  wtypeId = 9, traits = defaultTraits, etypeId = 1, animationId = 1, },
            new Weapon(){ description = "A bow", iconIndex= 990, name = "Bow", price = 350, _params = new int[] { 0, 0, 2, 0, 0, 0, 0, 0 },  wtypeId = 7, traits = defaultTraits, etypeId = 1, animationId = 1, },
            new Weapon(){ description = "A staff", iconIndex= 972, name = "Staff", price = 400, _params = new int[] { 0, 5, 1, 0, 3, 0, 0, 0 },  wtypeId = 6, traits = defaultTraits, etypeId = 1, animationId = 1, },
        };

        static Material[] materials =
        {
            new Material(){ name = "Rusty", level = 1, types = new int[] {2, 1, 4, 9}},
            new Material(){ name = "Iron", level = 4, types = new int[] {2, 1, 4, 9}},
            new Material(){ name = "Steel", level = 8, types = new int[] {2, 1, 4, 9}},
            new Material(){ name = "Hardened", level = 12, types = new int[] {2, 1, 4, 9}},
            new Material(){ name = "Darksteel", level = 16, types = new int[] {2, 1, 4, 9}},
            new Material(){ name = "Mythril", level = 20, types = new int[] {2, 1, 4, 9}},
            new Material(){ name = "Gnarled", level = 1, types = new int[] {7, 6}},
            new Material(){ name = "Birch", level = 4, types = new int[] {7, 6}},
            new Material(){ name = "Cedar", level = 8, types = new int[] {7, 6}},
            new Material(){ name = "Oak", level = 12, types = new int[] {7, 6}},
            new Material(){ name = "Heartwood", level = 16, types = new int[] {7, 6}},
            new Material(){ name = "Ebony", level = 20, types = new int[] {7, 6}},
        };

        public static IEnumerable<Weapon> GenerateWeapons()
        {
            List<Weapon?> weapons = new List<Weapon?>();
            weapons.Add(null);

            foreach (Material m in materials)
            {
                foreach (int i in m.types)
                {
                    Weapon prototype = prototypes.Where(x => x.wtypeId == i).First();
                    weapons.Add(prototype with
                    {
                        id = weapons.Count,
                        name = $"{m.name} {prototype.name}",
                        price = prototype.price * m.level,
                        _params = prototype._params.Select(x => x * m.level).ToArray()
                    });
                }
            }

            for (int i = weapons.Count; i < 100; i++)
            {
                weapons.Add(new Weapon() { id = i, name = "Blank" });
            }

            string customWeapons = File.ReadAllText("customweapons.json");
            IEnumerable<Weapon> cw = JsonConvert.DeserializeObject<IEnumerable<Weapon>>(customWeapons);
            foreach (Weapon w in cw)
            {
                weapons.Add(w with { id = weapons.Count });
            }

            return weapons;
        }
    }


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


}
