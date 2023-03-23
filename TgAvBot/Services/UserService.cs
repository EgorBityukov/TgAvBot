using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgAvBot.Data;
using TgAvBot.Models;

namespace TgAvBot.Services
{
    public class UserService : IUserService
    {
        private readonly IDataRepository _repository;

        private List<User> users { get; set; }

        public UserService(IDataRepository repository)
        {
            _repository = repository;
            users = _repository.GetDeSerializedUsers();
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public bool FindUser(long chatId)
        {
            if (users == null)
            {
                return false;
            }

            return users.Exists(u => u.ChatId == chatId);
        }

        public void AddUser(long chatId)
        {
            users.Add(new User { ChatId = chatId });
            _repository.SerializeUsers(users);
        }

        public void AddUrlForUser(long chatId, string url)
        {
            users.Where(u => u.ChatId == chatId).FirstOrDefault().URL = url;
            _repository.SerializeUsers(users);
        }

        public void RemoveUrlForUser(long chatId)
        {
            users.Where(u => u.ChatId == chatId).FirstOrDefault().URL = null;
            _repository.SerializeUsers(users);
        }
    }
}
