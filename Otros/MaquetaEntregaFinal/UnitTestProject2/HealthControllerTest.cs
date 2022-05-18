using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game;

namespace UnitTestProject2
{
    [TestClass]
    public class HealthControllerTest
    {
        [TestMethod]
        public void TestGetDamage()
        {
            int damage = 40;
            int maxHealt = 100;
            int expectedHealt = maxHealt - damage;

            HealthController healthController = new HealthController(maxHealt);
            healthController.GetDamage(damage);

            Assert.AreEqual(expectedHealt, healthController.CurrentHealth);
        }

        [TestMethod]
        public void TestGetHeal()
        {
            int heal = 20;
            int currentHealth = 80;
            int expectedHealt = currentHealth += heal;

            HealthController healthController = new HealthController(currentHealth);
            healthController.GetHeal(heal);

            Assert.AreEqual(expectedHealt, healthController.MaxLife);
        }
    }
}
