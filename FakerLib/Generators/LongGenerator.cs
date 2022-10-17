using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorPluginSupport;

namespace FakerLib.Generators
{
    class LongGenerator : IGenerator
    {
        public dynamic Generate()
        {
            Random r = new Random();
            return r.Next() << 32 | r.Next();
        }
        public Type GetTypeGenerator()
        {
            return typeof(long);
        }
    }
}
