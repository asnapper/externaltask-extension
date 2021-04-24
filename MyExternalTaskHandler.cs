using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ch.swisstxt.mh3.externaltask.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ch.swisstxt.mh3.externaltask
{
    public class MyExternalTaskHandler : ExternalTaskHandler<MyJob>
    {
        private readonly ILogger<MyExternalTaskHandler> logger;

        public MyExternalTaskHandler(ExternalTaskConfiguration configuration, ILogger<MyExternalTaskHandler> logger) : base(configuration, logger)
        {
            this.logger = logger;
        }

        public override void handleTask(ExternalTask<MyJob> task)
        {
            logger.LogInformation("starting", task);
            handleStart(task);

            logger.LogInformation("finishing", task);
            handleSuccess(task);
        }

    }
}
