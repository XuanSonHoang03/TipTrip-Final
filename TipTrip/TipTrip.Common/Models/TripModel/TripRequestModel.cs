using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipTrip.Common.Models.TripModel
{
    public class TripRequestModel
    {
        public string PickupPoint { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Transportation { get; set; }
        public decimal Budget { get; set; }
        public List<string> Preferences { get; set; } = new();
        public List<string> Avoid { get; set; } = new();
        public List<string> FavoriteFoods { get; set; } = new();
        public List<string> Activities { get; set; } = new();
        public int NumberOfPeople { get; set; }
    }

}
