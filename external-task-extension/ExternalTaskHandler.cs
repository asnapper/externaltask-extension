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
    public interface IExternalTaskHandler<TJob>
    {
        void handleError(ExternalTask<TJob> task, string errorMessage);
        void handleStart(ExternalTask<TJob> task);
        void handleSuccess(ExternalTask<TJob> task);
        void handleTask(ExternalTask<TJob> task);
    }

    public abstract class ExternalTaskHandler<TJob> : IExternalTaskHandler<TJob>, IHostedService
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

        private void sendToResultChannel(object payload)
        {
            IBasicProperties basicProperties = resultChannel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>();
            basicProperties.Headers.Add("contentType", "application/json");
            resultChannel.BasicPublish(exchange: "", routingKey: configuration.ResultChannel, basicProperties, body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload)));
        }

        public virtual void handleTask(ExternalTask<TJob> task) { }

        public void handleStart(ExternalTask<TJob> task)
        {

            var result = new ExternalTaskResult
            {
                externalTaskId = task.externalTaskId,
                topic = task.topic,
                status = ExternalTaskResultStatus.RUNNING,
                variables = task.variables
            };

            sendToResultChannel(result);
        }

        public void handleSuccess(ExternalTask<TJob> task)
        {

            var result = new ExternalTaskResult
            {
                externalTaskId = task.externalTaskId,
                topic = task.topic,
                status = ExternalTaskResultStatus.FINISHED,
                variables = task.variables
            };

            sendToResultChannel(result);
        }

        public void handleError(ExternalTask<TJob> task, string errorMessage)
        {

            var result = new ExternalTaskResult
            {
                externalTaskId = task.externalTaskId,
                topic = task.topic,
                status = ExternalTaskResultStatus.ERROR,
                variables = task.variables,
                errorMessage = errorMessage
            };

            sendToResultChannel(result);
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
                handleTask(task);
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


