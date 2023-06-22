using Foni.Code.Util;
using NUnit.Framework;

namespace Foni.Code.Tests.Util
{
    public class StructUtilsTests
    {
        private struct TestStruct
        {
            public int PublicField;
            public float PublicProperty { get; set; }
        }

        [Test]
        public void PrintStruct()
        {
            var testStruct = new TestStruct
            {
                PublicField = 5,
                PublicProperty = 13.37f
            };
            const string expected = "{ PublicProperty: 13.37, PublicField: 5 }";

            var result = StructUtils.PrintStruct(testStruct);

            Assert.AreEqual(expected, result);
        }
    }
}