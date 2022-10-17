﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.Generators
{
    class StringGenerator : IGenerator
    {
        public dynamic Generate()
        {
            Random r = new Random();
            byte[] tmp = new byte[r.Next(15) * 2];
            r.NextBytes(tmp);
            return Encoding.UTF8.GetString(tmp);
        }
        public Type GetTypeGenerator()
        {
            return typeof(string);
        }
    }
}
