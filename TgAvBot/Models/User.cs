using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgAvBot.Models
{
    [Serializable]
    public class User
    {

        public User()
        {

        }

        public User(long chatId)
        {
            ChatId = chatId;
        }

        [Key]
        public long ChatId {  get; set; }
        public string URL { get; set; }
    }
}
