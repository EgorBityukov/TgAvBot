using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgAvBot.Models
{
    public class Car
    {
        public long Id { get; set; }
        public string Url { get; set; }

        public Car(long id, string url)
        {
            Id = id;
            Url = url;
        }

        public override bool Equals(object obj)
        {
            Car car = obj as Car;
            return Id == car.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
