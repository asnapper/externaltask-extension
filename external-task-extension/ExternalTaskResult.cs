using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ch.swisstxt.mh3.externaltask.extension
{
    public class ExternalTaskResult
    {

        public string externalTaskId { get; set; }
        public string topic { get; set; }
        public ExternalTaskResultStatus status { get; set; }
        public string jobUrl { get; set; }
        public string errorMessage = null;
        public Dictionary<string, object> variables = new Dictionary<string, object>();

    }
}



