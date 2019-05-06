using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSABomber.Classes
{
    public class Bomb : IGameObject
    {
        public Vector2 Position { get; set; }
        public int Width { get; }
        public int Height { get; }
        public Vector2 Direction { get; set; }
        public BoxCollider Collider { get; set; }
        public bool IsDead { get; set; }
        public static int Radius = 120;
        public int LifeTimer = 120;
        public Type Type { get; }

        public Bomb(int xPos, int yPos, int width, int height)
        {
            Width = width;
            Height = height;
            Position = new Vector2(xPos, yPos);
            IsDead = false;
            Type = GetType();
        }


        public void Update(List<IGameObject> objects)
        {
            if (LifeTimer > 0)
                LifeTimer--;
            else
                Boom(objects);
        }

        private void Boom(IEnumerable<IGameObject> objects)
        {
            IsDead = true;
            var bombCenter = new Vector2(Position.X + Width / 2f, Position.Y + Height / 2f);
            foreach (var obj in objects)
            {
                var objCenter = new Vector2(obj.Position.X + obj.Width / 2f, obj.Position.Y + obj.Height / 2f);
                if ((objCenter - bombCenter).Length() < Radius && obj.GetType() != typeof(Wall))
                    obj.IsDead = true;
            }
        }
    }
}
