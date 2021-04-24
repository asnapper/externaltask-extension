using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ch.swisstxt.mh3.externaltask.Extension
{
    static class ExternalTaskExtension
    {
        public static void ConfigureExternalTasks<THandler>(this IServiceCollection services, ExternalTaskConfiguration configuration) where THandler : class, IHostedService{
            services.AddSingleton<ExternalTaskConfiguration>(configuration);
            services.AddHostedService<THandler>();
        }

        // public static void UseExternalTasks<THandler>(this IApplicationBuilder app, ExternalTaskConfiguration configuration) where THandler : class{
        //     // services.AddSingleton<ExternalTaskConfiguration>(configuration);
        //     // services.AddSingleton<THandler, THandler>();
        //     using(var scope=app.ApplicationServices.CreateScope()){
        //         scope.ServiceProvider
        //     }
        // }
    }
}
