﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.Generators
{
    class BoolGenerator:IGenerator
    {
        public dynamic Generate()
        {
            return Convert.ToBoolean(new Random().Next() & 1);
        }
        public Type GetTypeGenerator()
        {
            return typeof(bool);
        }
    }
}
