using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.Generators
{
    class LongGenerator : IGenerator<long>
    {
        public long Generate()
        {
            Random r = new Random();
            return r.Next() << 32 | r.Next();
        }
    }
}
