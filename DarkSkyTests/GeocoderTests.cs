using DarkSkyProject;
using NUnit.Framework;

namespace DarkSkyTests
{
    class GeocoderTests
    {
        Geocoder geocoder_;

        [SetUp]
        public void Setup()
        {
            geocoder_ = new Geocoder("dummy");
        }

        [Test]
        public void ShouldGetValidCoordinates()
        {
            var resp = geocoder_.GetCoordinates("", true, "");
            Assert.AreEqual(resp.Response.Message, "Please check the entered location");
        }

    }
}
