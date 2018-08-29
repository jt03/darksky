using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkSkyProject
{
    public class DarkSkyService : Service
    {
        private const string BaseUrl = "https://api.darksky.net/";
        private const string ApiKey = "YOUR API KEY ";
        private Geocoder geocoder = new Geocoder("YOUR API KEY");

        public DarkSkyService()
        {
        }

        public DarkSkyResponse Any(DarkSky request)
        {
            try
            {
                var errorMsgs = ValidateRequest(request);

                if (errorMsgs.Count > 0)
                    return new DarkSkyResponse { Message = string.Join(",",errorMsgs) };
                    
                var coordinates = geocoder.GetCoordinates(request.Location);

                if(coordinates.Response != null && coordinates.Response.Message != null)
                    return new DarkSkyResponse { Message = coordinates.Response.Message };

                var querystring = GetQueryFromRequest(request, coordinates.Latitude, coordinates.Longitude);
                var url = PrepareUrlWithQuery(request.ToPostUrl(), querystring);

                var response = url.GetJsonFromUrl();

                var darkSkyForecastResponse = response.FromJson<DarkSkyForecastResponse>();

                //since data for only 5 days is requested
                darkSkyForecastResponse.daily.data = darkSkyForecastResponse.daily.data.Take(5).ToList();

                return new DarkSkyResponse { DarkSkyForecastResponse = darkSkyForecastResponse };
            }
            catch (Exception ex)
            {
                return new DarkSkyResponse { Message = "Could not retrieve results. Please fill in the required fields." };
            }
        }

        // returns empty list if all validations pass
        private List<string> ValidateRequest(DarkSky request)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(request.Location))
                errors.Add("Please provide the location");
   
            // Other validations go here

            return errors;
        }

        // Assumes all validations on requets pass
        private string GetQueryFromRequest(DarkSky darkSkyRequest, double latitude, double longitude)
        {
            var block = darkSkyRequest.Block ?? "currently,minutely,hourly,alerts,flags";

            var request = String.Format("{0}/{1},{2}", ApiKey,
                latitude,
                longitude).AddQueryParam("exclude", block);

            if (!string.IsNullOrEmpty(darkSkyRequest.Units))
                request= request.AddQueryParam("units", darkSkyRequest.Units);
            return request;
        }

        private string PrepareUrlWithQuery(string postUrl, string queryString)
        {
            var relativeUrl = postUrl.Split('?')[0];
            return BaseUrl.CombineWith(relativeUrl).CombineWith(queryString);
        }
    }
}
