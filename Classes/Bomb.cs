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
        public List<IGameObject> Children { get; }
        public int Width { get; }
        public int Height { get; }
        public Vector2 Direction { get; set; }
        public BoxCollider Collider { get; set; }
        public bool IsDead { get; set; }
        public static int AOE = 120;
        public int lifeTimer = 120;
        private Image texture;
        private Color color;
        private Brush boomColor;

        public Bomb(int xPos, int yPos, int width, int height)
        {
            Width = width;
            Height = height;
            Position = new Vector2(xPos, yPos);
            Children = new List<IGameObject>();
            IsDead = false;
            texture = new Bitmap(Image.FromFile("Images\\bomb.png"), new Size(width, height));
            boomColor = new SolidBrush(Color.OrangeRed);
            color = Color.FromArgb(0, 255, 0, 0);
        }

        public void Update(List<IGameObject> objects)
        {
            if (lifeTimer > 0)
            {
                lifeTimer--;
                color = Color.FromArgb(240 - lifeTimer * 2, 255, 0, 0);
            }
            else
                Boom(objects);
        }

        public void Draw(Graphics g)
        {
            if (lifeTimer > 10)
            {
                g.DrawImage(texture, Position.X, Position.Y);
                return;
            }
            g.FillEllipse(boomColor, new RectangleF(Position.X + Width / 2 - AOE, 
                                                    Position.Y + Height / 2 - AOE, 
                                                    2 * AOE, 2 * AOE));
        }

        private void Boom(IEnumerable<IGameObject> objects)
        {
            IsDead = true;
            var bombCenter = new Vector2(Position.X + Width / 2f, Position.Y + Height / 2f);
            foreach (var obj in objects)
            {
                var objCenter = new Vector2(obj.Position.X + obj.Width / 2f, obj.Position.Y + obj.Height / 2f);
                if ((objCenter - bombCenter).Length() < AOE && obj.GetType() != typeof(Wall))
                    obj.IsDead = true;
            }
        }
    }
}
