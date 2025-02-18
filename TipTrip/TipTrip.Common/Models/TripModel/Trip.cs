using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipTrip.Common.Models.TripModel
{
    public class Trip
    {
        public int Id { get; set; }
        public string Destination { get; set; }  
        public DateTime StartDate { get; set; }  
        public DateTime EndDate { get; set; }    
        public string Transportation { get; set; } 
        public List<TripDay> Days { get; set; } = new List<TripDay>();
    }

    public class TripDay
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int DayNumber { get; set; } 
        public List<TripActivity> Activities { get; set; } = new List<TripActivity>();
        public List<WeatherInfo> Weather { get; set; } = new List<WeatherInfo>();
        public decimal Budget { get; set; }
        public string Note { get; set; }
    }

    public class TripActivity
    {
        public int Id { get; set; }
        public int TripDayId { get; set; }
        public TripDay TripDay { get; set; }
        public string Time { get; set; } 
        public string Location { get; set; } 
        public string RecommendedFood { get; set; } 
    }

    public class WeatherInfo
    {
        public int Id { get; set; }
        public int TripDayId { get; set; }
        public TripDay TripDay { get; set; }
        public float Temperature { get; set; } 
        public string Note { get; set; } 
    }

}
