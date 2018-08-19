using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DarkSkyProject
{
    [Route("/forecast", "POST")]
    public class DarkSky : IReturn<DarkSkyResponse>
    { 
        [Required]
        public string Location { get; set; }

        public string Block { get; set; }

        public string Units { get; set; }
    }

    public class DarkSkyResponse
    {
        public string Message { get; set; }

        public DarkSkyForecastResponse DarkSkyForecastResponse { get; set; }
    }

    public class CoordinatesResponse
    {
        public ResponseStatus Response { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Data
    {
        public int time { get; set; }
        public int precipIntensity { get; set; }
        public int precipProbability { get; set; }
    }

    public class Information
    {
        public DateTime DateTimeStamp
        {
            get
            {
                DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                long unixTimeStampInTicks = (long)(time * TimeSpan.TicksPerSecond);
                return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Local);
            }
        }

        public int time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public int sunriseTime { get; set; }
        public int sunsetTime { get; set; } 
        public double temperatureMin { get; set; }
        public int temperatureMinTime { get; set; }
        public double temperatureMax { get; set; }
        public int temperatureMaxTime { get; set; }
       
    }

    public class Daily
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<Information> data { get; set; }
    }

    public class DarkSkyForecastResponse
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string timezone { get; set; }
        public Daily daily { get; set; }  
    }

    public class Info
    {
        public int statuscode { get; set; }
        public List<object> messages { get; set; }
    }  

    public class ProvidedLocation
    {
        public string location { get; set; }
    }

    public class LatLng
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class DisplayLatLng
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Location
    {
        public string street { get; set; }      
        public LatLng latLng { get; set; }
        public DisplayLatLng displayLatLng { get; set; }
    }

    public class Result
    {
        public ProvidedLocation providedLocation { get; set; }
        public List<Location> locations { get; set; }
    }

    public class GeoCodingResponse
    {
        public Info info { get; set; }
        public List<Result> results { get; set; }
    }

}
