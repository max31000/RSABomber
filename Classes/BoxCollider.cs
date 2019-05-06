using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSABomber.Classes
{
    public class BoxCollider
    {
        public Rectangle Borders { get; set; }

        public BoxCollider(Vector2 pos, int width, int height)
        {
            Borders = new Rectangle((int)pos.X, (int)pos.Y, width, height);
        }

        public BoxCollider(int xPos, int yPos, int width, int height)
        {
            Borders = new Rectangle(xPos, yPos, width, height);
        }

        public bool IsCollision(BoxCollider r1)
        {
            return !(r1 is null) && Borders.IntersectsWith(r1.Borders);
        }
    }
}
