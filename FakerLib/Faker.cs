using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class Faker : IFaker
    {
        public T Create<T>()
        {
            

            return default(T);
        }
    }
}
