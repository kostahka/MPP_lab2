using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorPluginSupport;

namespace FakerLib.Generators
{
    class CharGenerator : IGenerator
    {
        public dynamic Generate()
        {
            return (char)new Random().Next(255);
        }
        public Type GetTypeGenerator()
        {
            return typeof(char);
        }
    }
}
