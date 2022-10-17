using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorPluginSupport;

namespace FakerLib.Generators
{
    class LongGenerator : Generator, IGenerator
    {
        public dynamic Generate(IFaker faker)
        {
            return rand.Next() << 32 | rand.Next();
        }
        public Type GetTypeGenerator()
        {
            return typeof(long);
        }
    }
}
