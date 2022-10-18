using GeneratorPluginSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.Generators
{
    public class CityGenerator : Generator, IGenerator
    {
        static string[] cities = new string[] { "Moscow" , "Minsk" , "Vileyka" };
        public dynamic Generate(IFaker faker)
        {
            return cities[rand.Next() % cities.Length];
        }
        public Type GetTypeGenerator()
        {
            return typeof(string);
        }
    }
}
