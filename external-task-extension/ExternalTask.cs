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
                return (TJob)Variables.GetValueOrDefault("job", null);
            }
        }

        public string Tenant
        {
            get
            {
                return (string)Variables.GetValueOrDefault("tenant", null);
            }
        }

        public long Priority { get; set; }

        // TODO: fix datetime de-/serialization between java Date and .net DateTime
        // [DataType(DataType.Date)]
        // [JsonConverter(typeof(JsonDateConverter))]
        // public DateTime lockExpirationTime { get; set; }
    }
}



