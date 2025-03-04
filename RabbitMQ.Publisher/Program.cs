using RabbitMQ.Client;
using System.Text;

// Create a connection
ConnectionFactory connectionFactory = new()
{
    Uri = new Uri("amqps://rhxapzxa:auD3cHM705kex3dbH2TdAZfDhuZfEPTq@cow.rmq2.cloudamqp.com/rhxapzxa")
};

// Create a channel
using IConnection connection =  connectionFactory.CreateConnection();
using var channel = connection.CreateModel();

// Create a queue

channel.QueueDeclare(queue :"my-queue", durable: true, exclusive: false, autoDelete: true, arguments: null);

// Send a message the queue

// RabitMQ accepts messages of type Byte for queuing.

//byte[] messageBodyBytes = Encoding.UTF8.GetBytes("Hello, World!");
//channel.BasicPublish(exchange: "", routingKey: "my-queue", basicProperties: null, body: messageBodyBytes);

IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true; 
for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Hello, World! {i}");
    channel.BasicPublish(exchange: "", routingKey: "my-queue", basicProperties: properties, body: messageBodyBytes);
}
Console.Read();

