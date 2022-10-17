using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.Generators
{
    class BoolGenerator:IGenerator<bool>
    {
        public bool Generate()
        {
            return Convert.ToBoolean(new Random().Next() & 1);
        }
    }
}
