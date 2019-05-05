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
        public List<IGameObject> Children { get; }
        public int Width { get; }
        public int Height { get; }
        public float Speed { get; set; }
        public Vector2 Direction { get; set; }
        public BoxCollider Collider { get; set; }
        public bool IsDead { get; set; }
        private Image texture;

        public Player(Vector2 pos, int width, int height)
        {
            Position = pos;
            Width = width;
            Height = height;
            Children = new List<IGameObject>();
            Direction = Vector2.Zero;
            Speed = 1.0f;
            texture = new Bitmap(Image.FromFile("Images\\nelson.png"), new Size(width, height));
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

            if (objects.Any(x => x != this && Collider.IsCollision(x.Collider)))
                Position -= Direction;
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(texture, Position.X, Position.Y);
        }
    }
}
