using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch.swisstxt.mh3.externaltask.extension
{
    public class ExternalTask<TJob>
    {
        public TJob job
        {
            get
            {
                object value;
                variables.TryGetValue("job", out value);
                return (TJob)value;
            }
        }

        public string tenant
        {
            get
            {
                object value;
                variables.TryGetValue("tenant", out value);
                return (string)value;
            }
        }

        public override string ToString() {
            return JsonSerializer.Serialize<ExternalTask<TJob>>(this);
        }


        public Dictionary<string, object> variables = new Dictionary<string, object>();
        public string topic { get; set; }
        public string externalTaskId { get; set; }
        public long priority { get; set; }
        
        // [DataType(DataType.Date)]
        // [JsonConverter(typeof(JsonDateConverter))]
        // public DateTime lockExpirationTime { get; set; }
    }
}



