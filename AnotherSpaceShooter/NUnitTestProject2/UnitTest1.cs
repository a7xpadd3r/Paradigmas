using NUnit.Framework;
using System.Numerics;

namespace NUnitTestProject2
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
            Vector2 posA = new Vector2(750, 280);
            Vector2 sizeA = new Vector2(32, 32);
            Vector2 posB = new Vector2(500, 900);
            Vector2 sizeB = new Vector2(50, 50);

            bool mb = Game.Collision.IsBoxColliding(posA, sizeA, posB, sizeB);
            Assert.AreEqual(false, mb);
        }

        [Test]
        public void ColisionCircleTest()
        {
            Vector2 posA = new Vector2(860, 400);
            float radA = 100.0f;
            Vector2 posB = new Vector2(860, 900);
            float radB = 80.12f;

            bool mb2 = Game.Collision.IsCircleColliding(posA, radA, posB, radB);
            Assert.AreEqual(true, mb2);
        }

    }
}