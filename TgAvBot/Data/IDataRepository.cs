using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgAvBot.Models;

namespace TgAvBot.Data
{
    public interface IDataRepository
    {
        public List<User> GetDeSerializedUsers();
        public void SerializeUsers(List<User> users);
        public HashSet<Car> GetDeSerializedCars();
        public void SerializeCars(HashSet<Car> cars);
    }
}
