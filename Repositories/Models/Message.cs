using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Message
    {
        public int Id { get; set; }
        [DisplayName("Message Content")]
        [Required]
        public string MessageContent { get; set; }

        [DisplayName("Message Sending Time")]
        public DateTime MessageSendingTime { get; set; } = DateTime.Now;

        [DisplayName("Message Author")]
        public string User { get; set; } = String.Empty;
    }
}
