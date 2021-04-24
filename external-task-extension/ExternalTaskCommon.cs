using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ch.swisstxt.mh3.externaltask.extension
{
    public class ExternalTaskCommon
    {
        [JsonConverter(typeof(DictionaryStringObjectJsonConverter))]
        public Dictionary<string, object> Variables { get; set; }

        public ExternalTaskCommon()
        {
            Variables = new Dictionary<string, object>();
        }

        public string Topic { get; set; }
        public string ExternalTaskId { get; set; }

    }
}
