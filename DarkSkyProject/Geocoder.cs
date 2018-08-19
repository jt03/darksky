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

        private CoordinatesResponse GetCoordinates(string location)
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

        // TODO: This probably could work with something like an internal modifier
        // https://stackoverflow.com/questions/358196/c-sharp-internal-access-modifier-when-doing-unit-testing
        public CoordinatesResponse GetCoordinates(string location, bool isTest = false, string dummyResponse = "")
        {
            if (isTest)
            {
                using (new HttpResultsFilter
                {
                    StringResult = "mocked"
                })
                {
                    return GetCoordinates(location);

                }
            }

            return GetCoordinates(location);
        }

    }
}