using Repositories.Models;

namespace Repositories.Contracts
{
    public interface IBotRepository
    {
        Task ConsulteTheBot(Message message);
    }
}
