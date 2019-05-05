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

        private static bool IsPartOverlap(int r1F, int r1S, int r2F, int r2S)
        {
            return Math.Max(r1F, r2F) <= Math.Min(r1S, r2S);
        }

        public bool IsCollision(BoxCollider r1)
        {
            if (r1 is null)
                return false;

            return IsPartOverlap(r1.Borders.Left, r1.Borders.Left + r1.Borders.Width, 
                                 Borders.Left, Borders.Left + Borders.Width) &&
                   IsPartOverlap(r1.Borders.Top, r1.Borders.Top + r1.Borders.Height, 
                                 Borders.Top, Borders.Top + Borders.Height);
        }
    }
}
