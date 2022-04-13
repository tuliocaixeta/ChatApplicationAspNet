using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Repositories.Contracts;
using Repositories.Models;
using Repositories.Repositories;
using Services.Services.interfaces;
using System.Text;

namespace Services.Services
{
    public class StockQuoteService: IStockQuoteService
    {
        public readonly IRepository<Message> messageRepository;
        public StockQuoteService(IRepository<Message> messageRepository) 
        {
            this.messageRepository = messageRepository;          
        }
        public async Task GetStockQuoteMesasge()
        {
            string message = "";
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "stock",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                   
                    var body = ea.Body.ToArray();
                    message = Encoding.UTF8.GetString(body);
                    messageRepository.Create( new Message { MessageContent = message, User = "Bot"});



                };
                channel.BasicConsume(queue: "stock",
                                autoAck: true,
                                consumer: consumer);
                // return new Message { MessageContent = message };

            }
        }

        
    }
}
