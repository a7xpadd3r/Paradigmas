using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game;

namespace UnitTestProject2
{
    [TestClass]
    public class SpeedBoostTest
    {
        [TestMethod]
        public void TestGetSpeed()
        {
            float speed = 20;
            float currentSpeed = 20;
            float expectedSpeed = currentSpeed += speed;

            SpeedBoostItem speedItem = new SpeedBoostItem (currentSpeed);
            speedItem.GetSpeed(currentSpeed);

            Assert.AreEqual(expectedSpeed, speedItem.CurrentSpeed);
        }
    }
}
