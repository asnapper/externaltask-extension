using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ch.swisstxt.mh3.externaltask.extension
{
    public static class ExternalTaskExtension
    {
        public static void ConfigureExternalTasks<THandler>(this IServiceCollection services, ExternalTaskConfiguration configuration) where THandler : class, IHostedService{
            services.AddSingleton<ExternalTaskConfiguration>(configuration);
            services.AddHostedService<THandler>();
        }

    }
}
