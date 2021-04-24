using System.Collections.Generic;

namespace ch.swisstxt.mh3.externaltask.extension
{
    public class ExternalTaskCommon
    {

        public Dictionary<string, object> Variables = new Dictionary<string, object>();
        public string Topic { get; set; }
        public string ExternalTaskId { get; set; }

    }
}
