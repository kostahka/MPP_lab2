using GeneratorPluginSupport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ListGeneratorFaker
{
    public class ListGenerator : Generator, IGenerator
    {
        public dynamic Generate(IFaker faker)
        {
            Type type = faker.GetCurrentType();

            Type[] tmp = type.GetGenericArguments();

            object newValue = this.GetType().GetMethod("GenerateList").MakeGenericMethod(tmp[0])
                                .Invoke(this, new[] { faker });

            return newValue;
        }
        public List<T> GenerateList<T>(IFaker faker)
        {
            List<T> tmp = new List<T>();
            int amount = rand.Next(10);
            for (int i = 0; i < amount; i++)
            {
                tmp.Add(faker.Create<T>());
            }
            return tmp;
        }
        public Type GetTypeGenerator()
        {
            return typeof(List<>);
        }
    }
}
