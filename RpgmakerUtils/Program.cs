using Newtonsoft.Json;
using System;

namespace RpgmakerUtils 
{
    internal class Program
    {


        static void Main(string[] args)
        {
            string output = JsonConvert.SerializeObject(WeaponGenerator.GenerateWeapons());
            File.WriteAllText("weapons.json", output);

            output = JsonConvert.SerializeObject(ArmorGenerator.GenerateArmor());
            File.WriteAllText("armors.json", output);
        }
    }
}