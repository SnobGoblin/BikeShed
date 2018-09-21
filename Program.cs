using System;
using System.Configuration;

namespace ChristiansOsloBySykkelClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Looking for Bysykkel!");

            CustomHttpClient client = new CustomHttpClient();
            var jsonString = client.GetBikeShedsAsync().Result;

            var dataPresenter = new DataPresenter();
            dataPresenter.InitBikeSheds(jsonString);

            jsonString = client.GetAvailabilitysAsync().Result;
            dataPresenter.InitAvailability(jsonString);

            dataPresenter.PrintStatus();

            Console.WriteLine("Press key to continue!");
            Console.ReadKey();
        }
    }
}
