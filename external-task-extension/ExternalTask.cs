using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch.swisstxt.mh3.externaltask.extension
{
    public class ExternalTask<TJob> : ExternalTaskCommon
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

        public long priority { get; set; }

        // TODO: fix datetime de-/serialization between java Date and .net DateTime
        // [DataType(DataType.Date)]
        // [JsonConverter(typeof(JsonDateConverter))]
        // public DateTime lockExpirationTime { get; set; }
    }
}



