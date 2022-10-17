using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorPluginSupport;

namespace FakerLib.Generators
{
    class DoubleGenerator : IGenerator
    {
        public dynamic Generate()
        {
            return new Random().NextDouble();
        }
        public Type GetTypeGenerator()
        {
            return typeof(double);
        }
    }
}
