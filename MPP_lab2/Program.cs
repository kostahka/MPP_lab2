using FakerLib;
using FakerLib.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_lab2
{
    class Program
    {
        class ParamCtorClass
        {
            public string i;
            double d;
            char c;

            public ParamCtorClass(double d)
            {
                this.d = d;
            }
        }

        class Circular1
        {
            public Circular2 c { get; set; }
        }
        class Circular2
        {
            public Circular3 c { get; set; }
        }
        class Circular3
        {
            public Circular1 c { get; set; }
        }

        class StringGenClass
        {
            public string s1;
            public string s2 { get; }
            public StringGenClass(string s2)
            {
                this.s2 = s2;
            }
        }

        struct SimpleStruct
        {
            public string field1;
            char field2;
        }
        class PrivSetClass
        {
            public int prop { get; }
            public long prop2 { get; set; }

            public PrivSetClass(int prop)
            {
                this.prop = prop;
            }
        }
        struct PubCtorStruct
        {
            public int field1;
            char field2;

            public PubCtorStruct(char f)
            {
                field2 = f;
                field1 = 1;
            }
        }

        class GenericClass
        {
            public List<int> numbers;
            public List<char> characters;
            public GenericClass(List<int> nums, List<char> chars)
            {
                numbers = nums;
                characters = chars;
            }
        }

        class Foo
        {
            public string City;
        }
        static void Main(string[] args)
        {
            FakerConfig fakerConfig = new FakerConfig();
            fakerConfig.Add<Foo, string, CityGenerator>(foo => foo.City);
            Faker f2 = new Faker(fakerConfig);

            var exp1 = f2.Create<Circular1>();
            var exp3 = f2.Create<PubCtorStruct>();
            var exp4 = f2.Create<StringGenClass>();
            var exp5 = f2.Create<SimpleStruct>();
            var exp6 = f2.Create<PrivSetClass>();
            var exp7 = f2.Create<int>();
            var exp8 = f2.Create<DateTime>();
            var exp9 = f2.Create<string>();
            var exp10 = f2.Create<GenericClass>();
            var exp11 = f2.Create<Foo>();

            Console.WriteLine("Complete");
            Console.ReadLine();
        }
    }
}
