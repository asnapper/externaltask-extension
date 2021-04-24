using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ch.swisstxt.mh3.externaltask.extension
{
    public class ExternalTaskResult : ExternalTaskCommon
    {

        public ExternalTaskResultStatus Status { get; set; }
        public string JobUrl { get; set; }

    }
}



