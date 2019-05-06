using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSABomber.Classes;

namespace RSABomber
{
    class Painter
    {
        public static BufferedGraphicsContext context = BufferedGraphicsManager.Current;
        public static BufferedGraphics buffer;
        private List<IGameObject> gameObjects;
        private Graphics graphics;
        private Dictionary<Type, Image> textures;
        private readonly Type wallType;
        private readonly Type playerType;
        private readonly Type stationType;
        private readonly Type bombType;

        public Painter(List<IGameObject> gameObjects, InGameForm gForm)
        {
            this.gameObjects = gameObjects;
            playerType = typeof(Player);
            wallType = typeof(Wall);
            stationType = typeof(Station);
            bombType = typeof(Bomb);
            graphics = gForm.CreateGraphics();
            buffer = context.Allocate(graphics, gForm.DisplayRectangle);
            LoadTextures();
        }

        private void LoadTextures()
        {
            textures = new Dictionary<Type, Image>
            {
                {wallType, new Bitmap(Image.FromFile("Images\\wall.png"), new Size(47, 47))},
                {playerType, new Bitmap(Image.FromFile("Images\\nelson.png"), new Size(32, 42))},
                {stationType, new Bitmap(Image.FromFile("Images/station.png"), 40, 40)},
                {bombType, new Bitmap(Image.FromFile("Images\\bomb.png"), new Size(40, 40))}
            };
        }

        public void Draw()
        {
            buffer.Graphics.Clear(Color.Gold);

            foreach (var gameObject in gameObjects)
            {
                if (gameObject.Type == wallType || gameObject.Type == stationType || gameObject.Type == playerType)
                    buffer.Graphics.DrawImage(textures[gameObject.Type], gameObject.Position.X, gameObject.Position.Y);

                if (gameObject.Type == bombType)
                    DrawBomb(gameObject);
            }

            buffer.Render();
        }

        private void DrawBomb(IGameObject gameObject)
        {
            var bomb = (Bomb)gameObject;

            if (bomb.LifeTimer > 10)
                buffer.Graphics.DrawImage(textures[gameObject.Type], bomb.Position.X, bomb.Position.Y);
            else
            {
                buffer
                    .Graphics
                    .FillEllipse(new SolidBrush(Color.OrangeRed),
                        new RectangleF(gameObject.Position.X + bomb.Width / 2 - Bomb.Radius,
                            bomb.Position.Y + bomb.Height / 2 - Bomb.Radius,
                            2 * Bomb.Radius, 2 * Bomb.Radius));
            }
        }
    }
}
