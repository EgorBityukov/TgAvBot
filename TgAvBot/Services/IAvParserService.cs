using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgAvBot.Models;

namespace TgAvBot.Services
{
    public interface IAvParserService
    {
        public List<Car> ParseCarsForUser(string url);
    }
}
