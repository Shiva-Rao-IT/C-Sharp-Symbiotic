using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

class Program
{
    static string connectionString = "YOUR_CONNECTION_STRING";
    static string queueName = "Servicequeue";

    static async Task Main()
    {
        await using ServiceBusClient client = new ServiceBusClient(connectionString);

        ServiceBusProcessor processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

        processor.ProcessMessageAsync += async args =>
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");

            await args.CompleteMessageAsync(args.Message);
        };

        processor.ProcessErrorAsync += args =>
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        };

        await processor.StartProcessingAsync();

        Console.WriteLine("Listening for queue messages...");
        Console.ReadKey();

        await processor.StopProcessingAsync();
    }
}

/* Reciever
using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

class Program
{
    static string connectionString = "YOUR_CONNECTION_STRING";
    static string queueName = "Servicequeue";

    static async Task Main()
    {
        await using ServiceBusClient client = new ServiceBusClient(connectionString);

        ServiceBusProcessor processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

        processor.ProcessMessageAsync += async args =>
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");

            await args.CompleteMessageAsync(args.Message);
        };

        processor.ProcessErrorAsync += args =>
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        };

        await processor.StartProcessingAsync();

        Console.WriteLine("Listening for queue messages...");
        Console.ReadKey();

        await processor.StopProcessingAsync();
    }
}
 */
