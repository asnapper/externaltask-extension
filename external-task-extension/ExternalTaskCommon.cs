using System.Collections.Generic;

namespace ch.swisstxt.mh3.externaltask.extension
{
    public class ExternalTaskCommon
    {

        public Dictionary<string, object> variables = new Dictionary<string, object>();
        public string topic { get; set; }
        public string externalTaskId { get; set; }

    }
}