using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TgAvBot.Data;
using TgAvBot.Models;

namespace TgAvBot.Services
{
    public class AvParserService : IAvParserService
    {
        private readonly IDataRepository _dataRepository;

        private HashSet<Car> cars = new HashSet<Car>();

        public AvParserService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            cars = _dataRepository.GetDeSerializedCars();
        }

        public List<Car> ParseCarsForUser(string url)
        {
            string html;

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        html = content.ReadAsStringAsync().Result;
                    }
                }
            }

            List<string> hrefTags = new List<string>();

            if (html != null)
            {
                HtmlDocument htmlSnippet = new HtmlDocument();
                htmlSnippet.LoadHtml(html);

                try
                {
                    var links = htmlSnippet.DocumentNode.SelectNodes("//a[contains(@class, 'listing-item__link')]");

                    if (links != null)
                    {
                        foreach (HtmlNode link in links)
                        {
                            HtmlAttribute att = link.Attributes["href"];
                            var carLink = "https://cars.av.by/" + att.Value;
                            hrefTags.Add(carLink);
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var newCars = AddCarRange(hrefTags);

            return newCars;
        }

        private List<Car> AddCarRange(List<string> carsString)
        {
            List<Car> newCars = new List<Car>();

            foreach (string car in carsString)
            {
                try
                {
                    long id = Convert.ToInt64(car.Substring(car.LastIndexOf("/") + 1));
                    var newCar = new Car(id, car);

                    if (!cars.Contains(newCar))
                    {
                        cars.Add(newCar);
                        newCars.Add(newCar);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            _dataRepository.SerializeCars(cars);

            return newCars;
        }
    }
}
