using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Data;
using Repositories.Models;

namespace Repositories.Repositories
{
    public class MessageRepository : IRepository<Message>
    {

        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext c)
        {
           this._context = c;
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            return await _context.Message.ToListAsync();
        }

        public async Task Create(Message message)
        {
            try
            {
                if (message != null)
                {
                    _context.Add(message);
                    await _context.SaveChangesAsync();
                }
            } catch (Exception)
            {

                throw new Exception();
            }
        }

        public async void Delete(int id)
        {
            var message = await _context.Message.FindAsync(id);
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
        }

        public async void Update(Message message)
        {
            _context.Update(message);
            await _context.SaveChangesAsync();
        }

        public async Task<Message> GetById (int? id)
        {
            return await _context.Message.FindAsync(id);
        }
    }

}
