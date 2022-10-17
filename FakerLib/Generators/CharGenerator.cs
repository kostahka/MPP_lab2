using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorPluginSupport;

namespace FakerLib.Generators
{
    class CharGenerator : Generator, IGenerator
    {
        public dynamic Generate(IFaker faker)
        {
            return (char)rand.Next(255);
        }
        public Type GetTypeGenerator()
        {
            return typeof(char);
        }
    }
}
