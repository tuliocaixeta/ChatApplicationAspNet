using Repositories.Contracts;
using Repositories.Models;
using Services.Services.interfaces;

namespace Services.Services
{
    public class MessageService : IMessageService
    {
        public readonly IRepository<Message> messageRepository;
        public MessageService(IRepository<Message> repository)
        {
            messageRepository = repository;
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
                await messageRepository.Create(message);
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
    }
}