using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.Generators
{
    class IntGenerator
    {
        public int Generate()
        {
            return new Random().Next();
        }
    }
}
