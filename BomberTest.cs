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

        [Test]
        public void TestLoadMap()
        {
            var objects = Load(@"Maps/1.map.txt");
            var stations = 0;
            var walls = 0;
            foreach (var obj in objects)
            {
                if (obj.GetType() == typeof(Wall))
                    walls++;
                if (obj.GetType() == typeof(Station))
                    stations++;
            }

            Assert.AreEqual(stations, 18);
            Assert.IsTrue(10 < walls);
        }

        [Test]
        public void TestPlayerWalk()
        {
            var objects = new List<IGameObject>();
            var player = new Player(new Vector2(0, 0), 20, 20)
                { Speed = 5f, Direction = new Vector2(1, 0)};
            objects.Add(new Wall(100, 0, 20, 100));
            for (var i = 0; i < 200; i++)
                player.Update(objects);
            Assert.IsTrue(player.Position.X > 74);
            Assert.IsTrue(player.Position.X < 81);
        }

        [Test]
        public void TestSuicide()
        {
            var objects = new List<IGameObject>();
            var player = new Player(new Vector2(0, 0), 20, 20) { Speed = 5f };
            objects.Add(player);
            var b = new Bomb(0, 0, 10, 10);
            objects.Add(b);
            for (var i = 0; i < 200; i++)
            {
                b.Update(objects);
                player.Update(objects);
            }

            Assert.IsTrue(player.IsDead);
        }

        [Test]
        public void TestDestroy()
        {
            var objects = new List<IGameObject>();
            var player = new Player(new Vector2(0, 0), 20, 20)
                { Speed = 10f, Direction = new Vector2(1,0)};
            objects.Add(player);
            var b = new Bomb(0, 0, 10, 10);
            var s = new Station(0, 30);
            objects.Add(b);
            objects.Add(s);
            for (var i = 0; i < 200; i++)
            {
                b.Update(objects);
                player.Update(objects);
            }

            Assert.IsFalse(player.IsDead);
            Assert.IsTrue(s.IsDead);
        }
    }
}