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
            geocoder_ = new Geocoder("YOUR API KEY");
        }

        [Test]
        public void ShouldGetValidCoordinates()
        {
            var resp = geocoder_.GetCoordinates("bellevu");
            Assert.IsNotNull(resp);
        }

    }
}
