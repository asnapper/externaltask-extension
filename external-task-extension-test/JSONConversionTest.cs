using System.IO;
using System.Text.Json;
using ch.swisstxt.mh3.externaltask.extension;
using Xunit;

namespace ch.swisstxt.mh3.externaltask.test
{

    public class JSONConversionTest
    {
        private class JSONConversionTestJob
        {
            public string field1 { get; set; }
            public string field2 { get; set; }

        }
        private string sampleJob { get; set; }

        private JsonSerializerOptions options { get; set; }
        public JSONConversionTest()
        {
            sampleJob = File.ReadAllText("JSONConversionTest_job.json");
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        [Fact]
        public void CanSerializeExternalTaskToJSON()
        {
            // TODO: how to easily verify json serialization
            // ExternalTask<JSONConversionTestJob> task = JsonSerializer.Deserialize<ExternalTask<JSONConversionTestJob>>(sampleJob);
            // Assert.NotNull(task);
        }

        [Fact]
        public void CanDeserializeExternalTaskFromJSON()
        {

            ExternalTask<JSONConversionTestJob> task = JsonSerializer.Deserialize<ExternalTask<JSONConversionTestJob>>(sampleJob, options);
            Assert.NotNull(task);
            Assert.NotNull(task.Job);
            Assert.Equal("first", task.Job.field1);
            Assert.Equal("second", task.Job.field2);
            Assert.Equal("Test", task.Tenant);
            Assert.Equal("mytopic", task.Topic);
            Assert.Equal("c3da00d6-abac-11ea-bd18-0a580a0a5169", task.ExternalTaskId);
            Assert.Equal(99, task.Priority);

        }
    }
}
