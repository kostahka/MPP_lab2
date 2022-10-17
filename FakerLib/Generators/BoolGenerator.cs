using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorPluginSupport;

namespace FakerLib.Generators
{
    class BoolGenerator:IGenerator
    {
        public dynamic Generate(IFaker faker)
        {
            return Convert.ToBoolean(new Random().Next() & 1);
        }
        public Type GetTypeGenerator()
        {
            return typeof(bool);
        }
        public void SetFakerInterface() { }
    }
}
