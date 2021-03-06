using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ch.swisstxt.mh3.externaltask.extension
{
    public abstract class ExternalTaskHandler<TJob> : IExternalTaskHandler<TJob>, IHostedService where TJob : new()
    {
        private readonly ExternalTaskConfiguration configuration;
        private readonly ILogger<ExternalTaskHandler<TJob>> logger;
        private readonly IModel resultChannel;
        private readonly IModel topicChannel;
        private readonly IConnection connection;
        public ExternalTaskHandler(ExternalTaskConfiguration configuration, ILogger<ExternalTaskHandler<TJob>> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
            var factory = new ConnectionFactory() { HostName = configuration.Host, Password = configuration.Password, UserName = configuration.User };

            connection = factory.CreateConnection();
            resultChannel = connection.CreateModel();
            topicChannel = connection.CreateModel();
        }
        private void SendToResultChannel(object payload)
        {
            IBasicProperties basicProperties = resultChannel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>();
            basicProperties.Headers.Add("contentType", "application/json");
            resultChannel.BasicPublish(exchange: "", routingKey: configuration.ResultChannel, basicProperties, body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload)));
        }

        public virtual void HandleTask(ExternalTask<TJob> task) { }

        public void HandleStart(ExternalTask<TJob> task)
        {

            var result = new ExternalTaskResult
            {
                ExternalTaskId = task.ExternalTaskId,
                Topic = task.Topic,
                Status = ExternalTaskResultStatus.RUNNING,
                Variables = task.Variables
            };

            SendToResultChannel(result);
        }

        public void HandleSuccess(ExternalTask<TJob> task)
        {

            var result = new ExternalTaskResult
            {
                ExternalTaskId = task.ExternalTaskId,
                Topic = task.Topic,
                Status = ExternalTaskResultStatus.FINISHED,
                Variables = task.Variables
            };

            SendToResultChannel(result);
        }

        public void HandleError(ExternalTask<TJob> task, string errorMessage)
        {

            var result = new ExternalTaskErrorResult
            {
                ExternalTaskId = task.ExternalTaskId,
                Topic = task.Topic,
                Status = ExternalTaskResultStatus.ERROR,
                Variables = task.Variables,
                ErrorMessage = errorMessage
            };

            SendToResultChannel(result);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            topicChannel.QueueDeclare(queue: configuration.TopicChannel, durable: false, exclusive: false, autoDelete: false, arguments: null);
            resultChannel.QueueDeclare(queue: configuration.ResultChannel, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(topicChannel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                ExternalTask<TJob> task = JsonSerializer.Deserialize<ExternalTask<TJob>>(message);
                logger.LogInformation("recieved task", task);
                HandleTask(task);
            };

            topicChannel.BasicConsume(queue: configuration.TopicChannel, autoAck: true, consumer: consumer);

            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            connection.Close();
            return Task.CompletedTask;
        }
    }
}


