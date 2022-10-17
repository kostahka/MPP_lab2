using FakerLib.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GeneratorPluginSupport;

namespace FakerLib
{
    public class Faker : IFaker
    {
        List<Type> DTOs = new List<Type>();
        Dictionary<Type, IGenerator> generators;
        Type currentType;

        public Type GetCurrentType()
        {
            return currentType;
        }
        public T Create<T>()
        {
            currentType = typeof(T);
            if (currentType.IsPrimitive || generators.ContainsKey(currentType))
            {
                try
                {
                    return (T)generators[currentType].Generate(this);
                }
                catch
                {
                    return default;
                }
            }
            else if (currentType.IsGenericType)
            {
                if(generators.ContainsKey(currentType.GetGenericTypeDefinition()))
                {
                    try
                    {
                        return (T)generators[currentType.GetGenericTypeDefinition()].Generate(this);
                    }
                    catch
                    {
                        return default;
                    }
                }
                else
                 return default;
            }
            else if (DTOs.Contains(currentType))
            {
                return default;
            }

            DTOs.Add(currentType);

            ConstructorInfo[] constructors = currentType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

            object constructed = default;
            if (currentType.IsValueType && constructors.Length == 0)
            {
                constructed = Activator.CreateInstance(typeof(T));
            }

            object[] ctorParams = null;
            ConstructorInfo ctor = null;

            foreach (ConstructorInfo cInfo in constructors.OrderByDescending(c => c.GetParameters().Length))
            {
                try
                {
                    ctorParams = GenerateCtorParams(cInfo);
                    constructed = cInfo.Invoke(ctorParams);
                    ctor = cInfo;
                    break;
                }
                catch { }
            }
            
            if(constructed != null)
                GenerateFieldsAndProperties(constructed, ctorParams, ctor);

            DTOs.Remove(currentType);

            return (T)constructed;
        }

        private void GenerateFieldsAndProperties(object constructed, object[] ctorParams, ConstructorInfo cInfo)
        {
            ParameterInfo[] pInfo = cInfo?.GetParameters();
            var fields = constructed.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                .Cast<MemberInfo>();
            var properties = constructed.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Cast<MemberInfo>();
            var fieldsAndProperties = fields.Concat(properties);

            foreach (MemberInfo m in fieldsAndProperties)
            {
                bool wasInitialized = false;


                Type memberType = (m as FieldInfo)?.FieldType ?? (m as PropertyInfo)?.PropertyType;
                object memberValue = (m as FieldInfo)?.GetValue(constructed) ??
                                     (m as PropertyInfo)?.GetValue(constructed);
                if (pInfo != null)
                {
                    for (int i = 0; i < ctorParams?.Length; i++)
                    {
                        if (ctorParams[i] == memberValue && memberType == pInfo[i].ParameterType)
                        {
                            wasInitialized = true;
                            break;
                        }
                    }
                }
                
                if (!wasInitialized)
                {
                    object newValue = this.GetType().GetMethod("Create").MakeGenericMethod(memberType)
                            .Invoke(this, null);

                    (m as FieldInfo)?.SetValue(constructed, newValue);
                    if ((m as PropertyInfo)?.CanWrite == true)
                    {
                        (m as PropertyInfo).SetValue(constructed, newValue);
                    }
                }
            }
        }

        private object[] GenerateCtorParams(ConstructorInfo cInfo)
        {
            ParameterInfo[] pInfo = cInfo.GetParameters();
            object[] ctorParams = new object[pInfo.Length];

            for (int i = 0; i < ctorParams.Length; i++)
            {
                Type fieldType = pInfo[i].ParameterType;

                object newValue = this.GetType().GetMethod("Create").MakeGenericMethod(fieldType)
                            .Invoke(this, null);

                ctorParams[i] = newValue;
            }

            return ctorParams;
        }

        public Faker()
        {
            generators = GetAllGenerators();
        }

        Dictionary<Type, IGenerator> GetAllGenerators()
        {
            Dictionary<Type, IGenerator> result = new Dictionary<Type, IGenerator>();

            Assembly asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes().
                        Where(t => t.GetInterfaces().
                        Where(i => i.FullName == typeof(IGenerator).FullName).Any());
            foreach (Type t in types)
            {
                IGenerator generator = (IGenerator)Activator.CreateInstance(t);
                result.Add(generator.GetTypeGenerator(), generator);
            }

            var plugins = PluginSupport.GetGeneratorsFromPlugins();
            foreach(var plugin in plugins.Values)
            {
                Type type = plugin.GetTypeGenerator();
                if (result.ContainsKey(type))
                    result.Remove(type);
                result.Add(type, plugin);
            }

            return result;
        }
    }
}
