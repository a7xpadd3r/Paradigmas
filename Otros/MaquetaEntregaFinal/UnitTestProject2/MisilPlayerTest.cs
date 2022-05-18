using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game;

namespace UnitTestProject2
{
    [TestClass]
    public class MisilPlayerTest
    {
        [TestMethod]
        public void TestPlayerReload()
        {
            int ammo = 10;
            int currentAmmo = 0;
            int expectedAmmo = currentAmmo += ammo;

            MisilPlayer misilPlayer = new MisilPlayer(0.5f, Bullet.MisilPlayer, currentAmmo);
            misilPlayer.Reload(ammo);

            Assert.AreEqual(expectedAmmo, misilPlayer.CurrentAmmo);
        }
    }
}
