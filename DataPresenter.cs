using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using static ChristiansOsloBySykkelClient.DataModel;

namespace ChristiansOsloBySykkelClient
{
    internal class DataPresenter
    {
        internal BikeSheds _bikeSheds;
        internal BikeShedsAvailability _availabilities;

        internal DataPresenter()
        {
        }

        internal void InitBikeSheds(string jsonData)
        {
            _bikeSheds = (BikeSheds)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData, typeof(BikeSheds));
        }

        internal void PrintStatus()
        {
            if (_bikeSheds == null)
            {
                Console.WriteLine("No bikeshed data available.");
                return;
            }

            foreach (BikeShed b in _bikeSheds.Stations)
            {
                Availability availability = GetAvailability(b.Id);
                if (availability == null)
                    Console.WriteLine("The bikeshed " + b.Title + " has no availability data available. No pun available.");
                else
                    PrintOneStatus(b.Title, availability.Bikes.ToString(), availability.Locks.ToString());
            }
        }

        private void PrintOneStatus(string name, string availableBikes, string availableLocks)
        {
            string availableBikesRegularNoun;
            string availableLocksRegularNoun;

            if (availableBikes == "1")
                availableBikesRegularNoun = "bike";
            else
                availableBikesRegularNoun = "bikes";

            if (availableLocks == "1")
                availableLocksRegularNoun = "lock";
            else
                availableLocksRegularNoun = "locks";


            Console.WriteLine("The bikeshed " + name +
                                  " has got " + availableBikes +
                                  " available " + availableBikesRegularNoun + " and " + availableLocks + 
                                  " available " + availableLocksRegularNoun + ".");
        }

            private Availability GetAvailability(int id)
        {
            foreach (AvailabilityStation a in _availabilities.Stations)
            {
                if (a.Id == id)
                    return a.availability;
            }
            return null;
        }

        internal void InitAvailability(string jsonData)
        {
            _availabilities = (BikeShedsAvailability)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData, typeof(BikeShedsAvailability));

        }
    }
}
