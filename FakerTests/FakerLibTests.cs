using FakerLib;
using FakerLib.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FakerTests
{
    [TestClass]
    public class FakerLibTests
    {
        private static Faker faker;


        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            faker = new Faker();
        }

        struct TestStruct
        {
            public int field1;
            char field2;
        }

        struct PrivTestStruct
        {
            public int field1;
            char field2;

            PrivTestStruct(char f)
            {
                field2 = f;
                field1 = 1;
            }
        }

        struct MultCtorStruct
        {
            int field1;
            char field2;
            public float field3;

            public MultCtorStruct(int field)
            {
                field1 = field;
                field2 = '1';
                field3 = 0.1f;
            }

            public MultCtorStruct(int field1, char field2, float field3)
            {
                this.field1 = field1;
                this.field2 = field2;
                this.field3 = field3;
            }
        }

        class TestClass
        {
            public int i;
            float f;
            public string s;
            public DateTime t;
            long l;
            public bool b;
            double d;
        }

        class PrivTestClass
        {
            public int i;
            float f;
            public string s;
            public DateTime t;
            long l;
            public char c;
            public bool b;
            double d;

            PrivTestClass(float f, long l)
            {
                this.f = f;
                this.l = l;
            }
        }

        class MultCtorClass
        {
            public int i;
            float f;
            public string s { get; set; }
            public DateTime t;
            long l;
            public char c { get; }
            public bool b { get; }
            double d;

            public MultCtorClass()
            {
            }

            public MultCtorClass(long l, double d)
            {
                this.l = l;
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

        class PrivSetClass
        {
            public int prop { get; }
            public long prop2 { get; set; }

            public PrivSetClass(int prop)
            {
                this.prop = prop;
            }
        }


        [TestMethod]
        public void CreateTestStruct()
        {
            var actual = faker.Create<TestStruct>();
            var notExpected = new TestStruct();
            Assert.AreNotEqual(notExpected.field1, actual.field1);
        }

        [TestMethod]
        public void CreatePrivTestrStruct()
        {
            var actual = faker.Create<PrivTestStruct>();
            var notExpected = new PrivTestStruct();
            Assert.AreNotEqual(notExpected.field1, actual.field1);
        }

        [TestMethod]
        public void CreateMultCtorStruct()
        {
            var actual = faker.Create<MultCtorStruct>();
            var notExpected = new MultCtorStruct();
            Assert.AreNotEqual(notExpected.field3, actual.field3);
        }

        [TestMethod]
        public void CreateTestClass()
        {
            var actual = faker.Create<TestClass>();
            var notExpected = new TestClass();
            Assert.AreNotEqual(notExpected.i, actual.i);
            Assert.AreNotEqual(notExpected.s, actual.s);
            Assert.AreNotEqual(notExpected.t, actual.t);
        }

        [TestMethod]
        public void CreatePrivCtorClass()
        {
            var actual = faker.Create<PrivTestClass>();
            PrivTestClass Expected = default;
            Assert.AreEqual(Expected, actual);
        }

        [TestMethod]
        public void CreateMultCtorClass()
        {
            var actual = faker.Create<MultCtorClass>();
            var notExpected = new MultCtorClass();
            Assert.AreEqual(notExpected.c, actual.c);
            Assert.AreNotEqual(notExpected.i, actual.i);
            Assert.AreNotEqual(notExpected.s, actual.s);
            Assert.AreNotEqual(notExpected.t, actual.t);
        }
        [TestMethod]
        public void CreateCircularClass()
        {
            var actual = faker.Create<Circular1>();
            Circular1 Expected = default;
            Assert.AreEqual(Expected, actual.c.c.c);
        }
    }
}
