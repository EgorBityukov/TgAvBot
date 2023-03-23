using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgAvBot.Models;

namespace TgAvBot.Services
{
    public interface IUserService
    {
        public void AddUser(long chatId);
        public bool FindUser(long chatId);
        public void AddUrlForUser(long chatId, string url);
        public void RemoveUrlForUser(long chatId);
        public List<User> GetUsers();
    }
}
