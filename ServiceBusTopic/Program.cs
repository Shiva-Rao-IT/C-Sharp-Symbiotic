using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

class Program
{
    static string connectionString = "YOUR_CONNECTION_STRING";
    static string topicName = "demotopic";

    static async Task Main()
    {
        await using ServiceBusClient client = new ServiceBusClient(connectionString);
        ServiceBusSender sender = client.CreateSender(topicName);

        for (int i = 1; i <= 5; i++)
        {
            var message = new ServiceBusMessage($"Topic Message {i}");
            await sender.SendMessageAsync(message);
            Console.WriteLine($"Published: Topic Message {i}");
        }

        Console.WriteLine("All messages published.");
    }
}



/* Reciever/Subscriber
using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

class Program
{
    static string connectionString = "YOUR_CONNECTION_STRING";
    static string topicName = "demotopic";
    static string subscriptionName = "sub1";

    static async Task Main()
    {
        await using ServiceBusClient client = new ServiceBusClient(connectionString);

        ServiceBusProcessor processor = client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions());

        processor.ProcessMessageAsync += async args =>
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received in {subscriptionName}: {body}");

            await args.CompleteMessageAsync(args.Message);
        };

        processor.ProcessErrorAsync += args =>
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        };

        await processor.StartProcessingAsync();

        Console.WriteLine($"Listening on subscription: {subscriptionName}");
        Console.ReadKey();

        await processor.StopProcessingAsync();
    }
}
 */