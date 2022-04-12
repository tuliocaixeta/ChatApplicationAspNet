using Newtonsoft.Json;
using Repositories.Contracts;
using Repositories.Models;
using System.Text;

namespace Repositories.Repositories
{
    public class BotRepository : IBotRepository
    { 
        private readonly string URL = "https://localhost:7027/ReceiveBotCommand";
        public async Task ConsulteTheBot(Message message)
        {
            try
            {
                if (message != null)
                {
                    var json = JsonConvert.SerializeObject(message);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    using var client = new HttpClient();
                    var response = await client.PostAsync(URL, data);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

    }
}
