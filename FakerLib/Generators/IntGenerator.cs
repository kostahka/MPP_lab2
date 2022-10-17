using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorPluginSupport;

namespace FakerLib.Generators
{
    class IntGenerator : IGenerator
    {
        public dynamic Generate(IFaker faker)
        {
            return new Random().Next();
        }
        public Type GetTypeGenerator()
        {
            return typeof(int);
        }
    }
}
