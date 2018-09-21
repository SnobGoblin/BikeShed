using System;
using System.Collections.Generic;

namespace ChristiansOsloBySykkelClient
{
    public class DataModel
    {

        public class BikeSheds
        {
            public List<BikeShed> Stations { get; set; }
        }

        public class BikeShed
        {
            public int Id;
            public string Title;
            public int Number_Of_Locks;
        }

        public class BikeShedsAvailability
        {
            public List<AvailabilityStation> Stations { get; set; }
        }

        public class AvailabilityStation
        {
            public int Id;
            public Availability availability;
        }

        public class Availability
        {
            public int Bikes;
            public int Locks;
        }

    }
}
