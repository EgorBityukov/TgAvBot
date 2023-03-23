using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgAvBot.Models;

namespace TgAvBot.Data
{
    public class DataRepository : IDataRepository
    {
        public List<User> GetDeSerializedUsers()
        {
            List<User> users = new List<User>();

            if (File.Exists("users.json"))
            {
                JsonSerializer serializer = new JsonSerializer();

                using (StreamReader sr = new StreamReader("users.json"))
                using (JsonReader jsonReader = new JsonTextReader(sr))
                {
                    var usersDeserealized = serializer.Deserialize<List<User>>(jsonReader);
                    if (usersDeserealized != null && usersDeserealized.Count > 0)
                    {
                        users = usersDeserealized;
                    }
                }
            }

            return users;
        }

        public void SerializeUsers(List<User> users)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter("users.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, users);
            }
        }

        public HashSet<Car> GetDeSerializedCars()
        {
            HashSet<Car> cars = new HashSet<Car>();

            if (File.Exists("cars.json"))
            {
                JsonSerializer serializer = new JsonSerializer();

                using (StreamReader sr = new StreamReader("cars.json"))
                using (JsonReader jsonReader = new JsonTextReader(sr))
                {
                    var carsDeserealized = serializer.Deserialize<HashSet<Car>>(jsonReader);
                    if (carsDeserealized != null && carsDeserealized.Count > 0)
                    {
                        cars = carsDeserealized;
                    }
                }
            }

            return cars;
        }

        public void SerializeCars(HashSet<Car> cars)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter("cars.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, cars);
            }
        }
    }
}
