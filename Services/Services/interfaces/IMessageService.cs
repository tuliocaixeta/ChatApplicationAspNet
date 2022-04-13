using Repositories.Models;

namespace Services.Services.interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetChatMessages();
        Task SendMessage(Message message);
        Task<Message> GetMessageById(int? id);
        void EditMessage(Message message);
        void DeleteMessage(int id);
        Task GetStockQuote();
    }
}
