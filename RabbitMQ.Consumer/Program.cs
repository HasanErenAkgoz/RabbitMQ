

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory connectionFactory = new()
{
    Uri = new Uri("amqps://rhxapzxa:auD3cHM705kex3dbH2TdAZfDhuZfEPTq@cow.rmq2.cloudamqp.com/rhxapzxa")
};

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.QueueDeclare(queue: "my-queue", durable: true, exclusive: false, autoDelete: true, arguments: null);


EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "my-queue", autoAck: false, consumer: consumer);
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
consumer.Received += (sender, eventArgs) =>
{
    byte[] body = eventArgs.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
    channel.BasicAck(eventArgs.DeliveryTag, multiple: false);

};

Console.Read();