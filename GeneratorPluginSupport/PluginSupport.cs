using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorPluginSupport
{
    public class PluginSupport
    {
        public static Dictionary<Type, IGenerator> GetGeneratorsFromPlugins()
        {
            Dictionary<Type, IGenerator> result = new Dictionary<Type, IGenerator>();

            string pluginsPath = Directory.GetCurrentDirectory() + "\\FakerLib Plugins\\Generators\\";
            if (!Directory.Exists(pluginsPath))
            {
                Directory.CreateDirectory(pluginsPath);
            }

            foreach (string str in Directory.GetFiles(pluginsPath, "*.dll"))
            {
                Assembly asm = Assembly.LoadFrom(str);
                var types = asm.GetTypes().
                        Where(t => t.GetInterfaces().
                        Where(i => i.FullName == typeof(IGenerator).FullName).Any());
                foreach (Type t in types)
                {
                    IGenerator generator = (IGenerator)Activator.CreateInstance(t);
                    if(!result.ContainsKey(generator.GetTypeGenerator()))
                        result.Add(generator.GetTypeGenerator(), generator);
                }
            }

            return result;
        }
    }
}
