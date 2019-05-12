using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSABomber.Classes
{
    public class Player : IGameObject
    {
        public Vector2 Position { get; set; }
        public int Width { get; }
        public int Height { get; }
        public float Speed { get; set; }
        public Vector2 Direction { get; set; }
        public BoxCollider Collider { get; set; }
        public bool IsDead { get; set; }
        public Type Type { get; }

        public Player(Vector2 pos, int width, int height)
        {
            Position = pos;
            Width = width;
            Height = height;
            Direction = Vector2.Zero;
            Speed = 1.0f;
            Type = GetType();
            Collider = new BoxCollider(Position, width - 5, height - 10);
            IsDead = false;
        }


        public void Update(List<IGameObject> objects)
        {
            if (Direction == Vector2.Zero)
                return;
            
            Direction = Vector2.Normalize(Direction) * Speed;
            Position += Direction;
            Collider.Borders = new Rectangle((int)Position.X + 2, (int)Position.Y + 5, Width - 5, Height - 10);

            if (objects.Any(x => x != this && x.Type != typeof(Bomb) && Collider.IsCollision(x.Collider)))
                Position -= Direction;
        }
    }
}
