﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.Generators
{
    class CharGenerator : IGenerator<char>
    {
        public char Generate()
        {
            return (char)new Random().Next(255);
        }
    }
}