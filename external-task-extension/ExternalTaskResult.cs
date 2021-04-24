using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ch.swisstxt.mh3.externaltask.extension
{
    public class ExternalTaskResult : ExternalTaskCommon
    {

        public ExternalTaskResultStatus status { get; set; }
        public string jobUrl { get; set; }
        public string errorMessage = null;

    }
}



