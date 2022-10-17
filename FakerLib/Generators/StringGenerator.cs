using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorPluginSupport;

namespace FakerLib.Generators
{
    class StringGenerator : Generator, IGenerator
    {
        public dynamic Generate(IFaker faker)
        {
            byte[] tmp = new byte[rand.Next(15) * 2];
            rand.NextBytes(tmp);
            return Encoding.UTF8.GetString(tmp);
        }
        public Type GetTypeGenerator()
        {
            return typeof(string);
        }
    }
}
