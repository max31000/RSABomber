using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSABomber.Classes
{
    public class Station : IGameObject
    {
        public Vector2 Position { get; set; }
        public List<IGameObject> Children { get; }
        public int Width { get; }
        public int Height { get; }
        public Vector2 Direction { get; set; }
        public BoxCollider Collider { get; set; }
        public bool IsDead { get; set; }
        private Image texture;

        public Station(int xPos, int yPos)
        {
            texture = new Bitmap(Image.FromFile("Images/station2.png"), 40, 40);
            Width = 40;
            Height = 40;
            Position = new Vector2(xPos, yPos);
            Children = new List<IGameObject>();
            Collider = new BoxCollider(xPos, yPos, 40, 40);
            IsDead = false;
        }

        public void Update(List<IGameObject> objects){ }

        public void Draw(Graphics g)
        {
            g.DrawImage(texture, Position.X, Position.Y);
        }
    }
}
