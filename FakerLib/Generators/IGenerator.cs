using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.Generators
{
    public interface IGenerator
    {
        dynamic Generate();
        Type GetTypeGenerator();
    }
}
