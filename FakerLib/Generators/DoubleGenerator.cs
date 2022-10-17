using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.Generators
{
    class DoubleGenerator : IGenerator<double>
    {
        public double Generate()
        {
            return new Random().NextDouble();
        }
    }
}
