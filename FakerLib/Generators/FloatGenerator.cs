using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.Generators
{
    class FloatGenerator : IGenerator<float>
    {
        public float Generate()
        {
            return (float)new Random().NextDouble();
        }
    }
}
