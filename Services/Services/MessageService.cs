using Repositories.Contracts;
using Repositories.Models;
using Services.Services.interfaces;

namespace Services.Services
{
    public class MessageService : IMessageService
    {
        public readonly IRepository<Message> messageRepository;
        public readonly IBotRepository botRepository;
        public readonly IStockQuoteService stockQuoteService;
        private readonly string BOT_COMMAND = "/stock";
        public MessageService(IRepository<Message> messageRepository, IBotRepository botRepository, IStockQuoteService stockQuoteService)
        {
            this.messageRepository = messageRepository;
            this.botRepository = botRepository;
            this.stockQuoteService = stockQuoteService;
        }

        public async Task<IEnumerable<Message>> GetChatMessages()
        {
            try
            {
                var messages = await messageRepository.GetAll();
                return messages;
            } catch (Exception ex)
            {
                throw;
            }
           
        }

        public async Task<Message> GetMessageById (int? id)
        {
            try
            {
                return await messageRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SendMessage(Message message)
        {
            try
            {
                if (validateStockBotCommand(message)) {
                    await botRepository.ConsulteTheBot(message);
                } else {
                    await messageRepository.Create(message);
                }
               
            }
            catch (Exception ex)
            {
                throw;
            }

        }

      

        
        public void DeleteMessage(int id )
        {
            try
            {
                messageRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void EditMessage(Message message)
        {
            try
            {
                messageRepository.Update(message);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task GetStockQuote()
        {
            try
            {
                await stockQuoteService.GetStockQuoteMesasge();
                //if(message.MessageContent != "")
                //{
                //    message.User = "Bot";
                //    await SendMessage(message);
                //}
                
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        private bool validateStockBotCommand(Message message)
        {
            return message.MessageContent.Contains(BOT_COMMAND);
        }
    }
}