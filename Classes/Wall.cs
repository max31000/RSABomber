using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSABomber.Classes
{
    class Wall : IGameObject
    {
        public Vector2 Position { get; set; }
        public int Width { get; }
        public int Height { get; }
        public Vector2 Direction { get; set; }
        public BoxCollider Collider { get; set; }
        public bool IsDead { get; set; }
        public Type Type { get; }

        public Wall(float xPos, float yPos, int width, int height)
        {
            Width = width;
            Height = height;
            Position = new Vector2(xPos, yPos);
            Type = GetType();
            Collider = new BoxCollider(Position, width, height);
            IsDead = false;
        }

        public void Update(List<IGameObject> objects) { }
    }
}
