using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch.swisstxt.mh3.externaltask.extension
{
    public class ExternalTask<TJob> : ExternalTaskCommon where TJob : new()
    {

        public TJob Job
        {
            get { return Variables.GetAs<TJob>("job"); }
        }

        public string Tenant
        {
            get { return (string)Variables["tenant"]; }
        }

        public long Priority { get; set; }

        // TODO: fix datetime de-/serialization between java Date and .net DateTime
        // [DataType(DataType.Date)]
        // [JsonConverter(typeof(JsonDateConverter))]
        // public DateTime lockExpirationTime { get; set; }
    }
}



