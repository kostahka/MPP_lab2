using FakerLib.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GeneratorPluginSupport;
using System.Linq.Expressions;

namespace FakerLib
{
    public class Faker : IFaker
    {
        List<Type> DTOs = new List<Type>();
        Dictionary<Type, IGenerator> generators;
        Stack<Type> currentType = new Stack<Type>();
        FakerConfig Config = null;

        public Type GetCurrentType()
        {
            return currentType.Peek();
        }
        public T Create<T>()
        {
            currentType.Push(typeof(T));
            if (typeof(T).IsPrimitive || generators.ContainsKey(typeof(T)))
            {
                T result;
                try
                {
                     result = (T)generators[typeof(T)].Generate(this);
                }
                catch
                {
                    result = default;
                }
                currentType.Pop();
                return result;
            }
            else if (currentType.Peek().IsGenericType)
            {
                if(generators.ContainsKey(typeof(T).GetGenericTypeDefinition()))
                {
                    T result;
                    try
                    {
                        result = (T)generators[typeof(T).GetGenericTypeDefinition()].Generate(this);
                    }
                    catch
                    {
                        result = default;
                    }
                    currentType.Pop();
                    return result;
                }
                else
                {
                    currentType.Pop();
                    return default;
                }
                 
            }
            else if (DTOs.Contains(typeof(T)))
            {
                currentType.Pop();
                return default;
            }

            DTOs.Add(typeof(T));

            ConstructorInfo[] constructors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.Public);

            object constructed = default;
            if (typeof(T).IsValueType && constructors.Length == 0)
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
            {
                GenerateFieldsAndProperties(constructed, ctorParams, ctor);
                if (Config != null && Config.expressions.ContainsKey(typeof(T)))
                {
                    var test = (Config.expressions[typeof(T)] as Expression<Action<T, IFaker>>);
                    test.Compile()((T)constructed, this);
                }
            }
                

            DTOs.Remove(typeof(T));

            currentType.Pop();
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

        public Faker(FakerConfig config)
        {
            generators = GetAllGenerators();
            Config = config;
        }

        Dictionary<Type, IGenerator> GetAllGenerators()
        {
            Dictionary<Type, IGenerator> result = new Dictionary<Type, IGenerator>();

            Assembly asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes().
                        Where(t => !t.IsPublic && t.GetInterfaces().
                        Where(i => (i.FullName == typeof(IGenerator).FullName)).Any());
            foreach (Type t in types)
            {
                IGenerator generator = (IGenerator)Activator.CreateInstance(t);
                Type type = generator.GetTypeGenerator();
                if (!result.ContainsKey(type))
                    result.Add(type, generator);
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
