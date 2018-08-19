using NUnit.Framework;
using DarkSkyProject;

namespace DarkSkyTests
{
    [TestFixture]
    public class DarkSkyTests
    {
        DarkSkyService darkSky_;

        [SetUp]
        public void Setup()
        {
            darkSky_ = new DarkSkyService();
        }


        [Test]
        public void GetWeatherWithRightData()
        {
            DarkSkyResponse response = darkSky_.Any(new DarkSkyProject.DarkSky {Location="bellevue wa" });
            Assert.IsNotNull(response.DarkSkyForecastResponse);
        }

        [Test]
        public void GetWeatherLocationNotPassed()
        {
            DarkSkyResponse response = darkSky_.Any(new DarkSkyProject.DarkSky { Location = "" });
            Assert.AreEqual(response.Message, "Please provide the location");
        }
    }
}
