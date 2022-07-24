using NUnit.Framework;
using System.Numerics;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ColisionBoxTest()
        {
            Vector2 position1 = new Vector2(750, 280);
            Vector2 size1 = new Vector2(32, 32);
            Vector2 position2 = new Vector2(750, 280);
            Vector2 size2 = new Vector2(50, 50);

            bool mb = Game.Collision.IsBoxColliding(position1, size1, position2, size2);
            Assert.AreEqual(true, mb);
        }

        [Test]
        public void ColisionCircleTest()
        {
            Vector2 position1 = new Vector2(860, 400);
            float radio1 = 100.0f;
            Vector2 position2 = new Vector2(860, 900);
            float radio2 = 80.12f;

            bool mb2 = Game.Collision.IsCircleColliding(position1, radio1, position2, radio2);
            Assert.AreEqual(true, mb2);
        }
    }
}