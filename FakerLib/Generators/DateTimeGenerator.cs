using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorPluginSupport;

namespace FakerLib.Generators
{
    class DateTimeGenerator : Generator, IGenerator
    {
        public dynamic Generate(IFaker faker)
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rand.Next(range));
        }
        public Type GetTypeGenerator()
        {
            return typeof(DateTime);
        }
    }
}
