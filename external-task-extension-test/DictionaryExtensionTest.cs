using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ch.swisstxt.mh3.externaltask.extension;
using Xunit;

namespace ch.swisstxt.mh3.externaltask.test
{

    public class DictionaryExtensionTest
    {

        private class TestComplexType
        {
            public string field1 { get; set; }
            public string field2 { get; set; }
        }

        private class TestSpecificType
        {
            public string stringItem { get; set; }

            public List<string> simpleListItem { get; set; }


            public List<TestComplexType> complexListItem { get; set; }
        }

        private Dictionary<string, object> testStringObjectDict = new Dictionary<string, object>
        {
            ["stringItem"] = "stringItemValue",
            ["simpleListItem"] = new List<string> { "first", "second" },
            ["complexListItem"] = new List<TestComplexType> { new TestComplexType { field1 = "1f1Value", field2 = "1f2Value" }, new TestComplexType { field1 = "2f1Value", field2 = "2f2Value" } }
        };

        private Dictionary<string, dynamic> testStringDynamicDict = new Dictionary<string, dynamic>
        {
            ["stringItem"] = "stringItemValue",
            ["simpleListItem"] = new List<string> { "first", "second" },
            ["complexListItem"] = new List<TestComplexType> { new TestComplexType { field1 = "1f1Value", field2 = "1f2Value" }, new TestComplexType { field1 = "2f1Value", field2 = "2f2Value" } }
        };

        [Fact]
        public void CanConvertStringObjectDictionaryToSpecificType()
        {
            var container = new Dictionary<string, object>
            {
                ["data"] = testStringObjectDict
            };

            var specific = container.GetAs<TestSpecificType>("data");

            Assert.NotNull(specific);
            Assert.Equal("stringItemValue", specific.stringItem);

            Assert.Equal(2, specific.simpleListItem.Count);
            Assert.Equal(2, specific.complexListItem.Count);

            Assert.Equal("first", specific.simpleListItem[0]);
            Assert.Equal("second", specific.simpleListItem[1]);

            Assert.Equal("1f1Value", specific.complexListItem[0].field1);
            Assert.Equal("2f1Value", specific.complexListItem[1].field1);

            Assert.Equal("1f2Value", specific.complexListItem[0].field2);
            Assert.Equal("2f2Value", specific.complexListItem[1].field2);

        }
        [Fact]
        public void CanConvertStringDynamicDictionaryToSpecificType()
        {
            var container = new Dictionary<string, dynamic>
            {
                ["data"] = testStringDynamicDict
            };

            var specific = container.GetAs<TestSpecificType>("data");

            Assert.NotNull(specific);
            Assert.Equal("stringItemValue", specific.stringItem);

            Assert.Equal(2, specific.simpleListItem.Count);
            Assert.Equal(2, specific.complexListItem.Count);

            Assert.Equal("first", specific.simpleListItem[0]);
            Assert.Equal("second", specific.simpleListItem[1]);

            Assert.Equal("1f1Value", specific.complexListItem[0].field1);
            Assert.Equal("2f1Value", specific.complexListItem[1].field1);

            Assert.Equal("1f2Value", specific.complexListItem[0].field2);
            Assert.Equal("2f2Value", specific.complexListItem[1].field2);

        }
        [Fact]
        public void CanConvertObjectToDictionaryAndBack()
        {
            var container = new Dictionary<string, dynamic>
            {
                ["data"] = testStringDynamicDict
            };

            var specific = container.GetAs<TestSpecificType>("data");

            var result = specific.ToDictionary<object>();

            Assert.NotNull(result);

            Assert.Equal(testStringDynamicDict, result);

            var container2 = new Dictionary<string, dynamic>
            {
                ["data"] = result
            };

            var specific2 = container.GetAs<TestSpecificType>("data");

            Assert.NotNull(specific2);

            Assert.Equal(specific.stringItem, specific2.stringItem);
            Assert.Equal(specific.simpleListItem, specific2.simpleListItem);
            Assert.Equal(specific.complexListItem, specific2.complexListItem);

        }

    }
}
