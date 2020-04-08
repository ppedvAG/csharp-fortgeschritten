using Grpc.Net.Client;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new GrpcService1.Greeter.GreeterClient(channel);
            var res = client.SayHello(new GrpcService1.HelloRequest() { Name = "Fred", Zahl = 12 });

            Console.WriteLine(res.Message);



            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
