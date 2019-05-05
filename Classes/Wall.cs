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
        public List<IGameObject> Children { get; }
        public int Width { get; }
        public int Height { get; }
        public Vector2 Direction { get; set; }
        public BoxCollider Collider { get; set; }
        public bool IsDead { get; set; }
        public Color Color { get; set; }
        private Brush b;
        private Image texture;

        public Wall(Vector2 pos, int width, int height)
        {
            Children = new List<IGameObject>();
            Width = width;
            Height = height;
            Position = pos;
            Color = Color.Blue;
            b = new SolidBrush(Color);
            Collider = new BoxCollider(pos, width, height);
            texture = new Bitmap(Image.FromFile("Images\\wall.png"), new Size(width, height));
            IsDead = false;
        }

        public Wall(float xPos, float yPos, int width, int height)
        {
            Children = new List<IGameObject>();
            Width = width;
            Height = height;
            Position = new Vector2(xPos, yPos);
            Color = Color.Coral;
            b = new SolidBrush(Color);
            Collider = new BoxCollider(Position, width, height);
            texture = new Bitmap(Image.FromFile("Images\\wall.png"), new Size(width, height));
            IsDead = false;
        }

        public void Update(List<IGameObject> objects) { }

        public void Draw(Graphics g)
        {
            g.DrawImage(texture, Position.X, Position.Y);
        }
    }
}
