using ServiceStack;
using System.Linq;

namespace DarkSkyProject
{
    public class Geocoder
    {
        public Geocoder(string geocodingKey)
        {
            GeoCodingKey = geocodingKey;
            
        }

        private string GeoCodingKey { get; }

        private const string BaseUrlGeoCoding = "http://www.mapquestapi.com/geocoding/v1/address/";

        public CoordinatesResponse GetCoordinates(string location)
        {
            try
            {
                var url = BaseUrlGeoCoding.AddQueryParam("key", GeoCodingKey).AddQueryParam("location", location);
                var response = url.GetJsonFromUrl();

                var geoCodingResponse = response.FromJson<GeoCodingResponse>();

                if (geoCodingResponse != null && geoCodingResponse.info.statuscode == 0)
                {
                    var data = geoCodingResponse.results.FirstOrDefault().locations.FirstOrDefault();
                    return new CoordinatesResponse { Latitude = data.latLng.lat, Longitude = data.latLng.lng };
                }
                return new CoordinatesResponse { Response = new ResponseStatus { Message = "Please check the entered location" } };
            }
            catch
            {
                return new CoordinatesResponse { Response = new ResponseStatus { Message = "Some Error Occurred while processing." } };
            }
        }      
    }
}