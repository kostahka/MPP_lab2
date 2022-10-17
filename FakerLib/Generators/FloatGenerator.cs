using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorPluginSupport;

namespace FakerLib.Generators
{
    class FloatGenerator : Generator, IGenerator
    {
        public dynamic Generate(IFaker faker)
        {
            return (float)rand.NextDouble();
        }
        public Type GetTypeGenerator()
        {
            return typeof(float);
        }
    }
}
