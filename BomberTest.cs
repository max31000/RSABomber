using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RSABomber.Classes;
using static RSABomber.MapLoader;
using RSABomber;

namespace RSABomberTests
{
    [TestFixture]
    public class RSABomberTest
    {
        [Test]
        public void IsPointOutsideRectTest()
        {
            var rWidth = 18;
            var rHeight = 6;
            var rPos = new Vector2(0, 0);
            var point = new Vector2(37, 42);
            Assert.IsFalse(Calculator.DotIsInsideRect(point, rPos, rWidth, rHeight));
        }

        [Test]
        public void IsPointInsideRectTest()
        {
            var rWidth = 18;
            var rHeight = 6;
            var rPos = new Vector2(0, 0);
            var point = new Vector2(1, 1);
            Assert.IsTrue(Calculator.DotIsInsideRect(point, rPos, rWidth, rHeight));
        }

        [Test]
        public void IsSquareRightTest()
        {
            Vector2 v1 = new Vector2(1, 0);
            Vector2 v2 = new Vector2(0, 5);
            Assert.AreEqual(Calculator.GetParallelogramSquare(v1, v2), 5, 0.1);
        }

        [Test]
        public void CollisionsIntersect()
        {
            var c1 = new BoxCollider(1, 1, 5, 5);
            var c2 = new BoxCollider(4, 4, 5, 5);
            Assert.IsTrue(c1.IsCollision(c2));
            Assert.IsTrue(c2.IsCollision(c1));
        }

        [Test]
        public void CollisionsNotIntersect()
        {
            var c1 = new BoxCollider(1, 1, 5, 5);
            var c2 = new BoxCollider(7, 7, 5, 5);
            Assert.IsFalse(c1.IsCollision(c2));
            Assert.IsFalse(c2.IsCollision(c1));
        }
    }
}