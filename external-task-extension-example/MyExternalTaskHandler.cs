using ch.swisstxt.mh3.externaltask.extension;
using Microsoft.Extensions.Logging;

namespace ch.swisstxt.mh3.externaltask.example
{
    public class MyExternalTaskHandler : ExternalTaskHandler<MyJob>
    {
        private readonly ILogger<MyExternalTaskHandler> logger;

        public MyExternalTaskHandler(ExternalTaskConfiguration configuration, ILogger<MyExternalTaskHandler> logger) : base(configuration, logger)
        {
            this.logger = logger;
        }

        public override void HandleTask(ExternalTask<MyJob> task)
        {
            logger.LogInformation("starting", task);
            HandleStart(task);

            logger.LogInformation("finishing", task);
            HandleSuccess(task);
        }

    }
}
