using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorPluginSupport
{
    public interface IGenerator
    {
        dynamic Generate(IFaker faker);
        Type GetTypeGenerator();
    }
}
