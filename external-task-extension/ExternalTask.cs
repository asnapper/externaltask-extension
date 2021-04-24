using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch.swisstxt.mh3.externaltask.extension
{
    public class ExternalTask<TJob> : ExternalTaskCommon
    {
        public TJob Job
        {
            get
            {
                object value;
                Variables.TryGetValue("job", out value);
                return (TJob)value;
            }
        }

        public string Tenant
        {
            get
            {
                object value;
                Variables.TryGetValue("tenant", out value);
                return (string)value;
            }
        }

        public long Priority { get; set; }

        // TODO: fix datetime de-/serialization between java Date and .net DateTime
        // [DataType(DataType.Date)]
        // [JsonConverter(typeof(JsonDateConverter))]
        // public DateTime lockExpirationTime { get; set; }
    }
}



