using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSABomber.Classes
{
    class Enemy : IGameObject
    {
        public Vector2 Position { get; set; }
        public int Width { get; }
        public int Height { get; }
        public Vector2 Direction { get; set; }
        public BoxCollider Collider { get; set; }
        public bool IsDead { get; set; }
        public Type Type { get; }
        private Random rand;

        public Enemy(int xPos, int yPos, int width, int height)
        {
            Width = width;
            Height = height;
            Position = new Vector2(xPos, yPos);
            Direction = new Vector2(1, 0);
            Collider = new BoxCollider(Position + new Vector2(5, 5), width - 10, height - 10);
            Type = typeof(Enemy);
            rand = new Random();
        }

        public void Update(List<IGameObject> objects)
        {
            if (Direction == Vector2.Zero)
                return;

            Direction = Vector2.Normalize(Direction);
            Position += Direction;
            Collider.Borders = new Rectangle((int)Position.X + 10, (int)Position.Y + 6, Width - 20, Height - 12);

            if (objects.Any(x => x != this && Collider.IsCollision(x.Collider)))
            {
                Position -= Direction;
                if (rand.Next(0, 2) == 1)
                    Direction = -Direction;
                else
                    Direction = new Vector2(Direction.Y, -Direction.X);
            }

            foreach (var obj in objects)
            {
                if (obj.Type == typeof(Player) && obj.Collider.IsCollision(Collider))
                    obj.IsDead = true;
            }
        }
    }
}
