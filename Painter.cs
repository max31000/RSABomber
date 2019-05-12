using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        private readonly Type ghostType;
        private readonly Type stationType;
        private readonly Type bombType;
        private Image grassTexture; 

        public Painter(List<IGameObject> gameObjects, Control gForm)
        {
            this.gameObjects = gameObjects;
            playerType = typeof(Player);
            wallType = typeof(Wall);
            stationType = typeof(Station);
            bombType = typeof(Bomb);
            ghostType = typeof(Enemy);
            graphics = gForm.CreateGraphics();
            buffer = context.Allocate(graphics, gForm.DisplayRectangle);
            LoadTextures();
        }

        private void LoadTextures()
        {
            grassTexture = new Bitmap(Image.FromFile("Images\\grass.jpg"), 47, 47);
            textures = new Dictionary<Type, Image>
            {
                {wallType, new Bitmap(Image.FromFile("Images\\wall.png"), new Size(47, 47))},
                {playerType, new Bitmap(Image.FromFile("Images\\nelson.png"), new Size(32, 42))},
                {stationType, new Bitmap(Image.FromFile("Images/station.png"), 40, 40)},
                {bombType, new Bitmap(Image.FromFile("Images\\bomb.png"), new Size(40, 40))},
                {ghostType, new Bitmap(Image.FromFile("Images\\ghost.png"), 40, 40) }
            };
        }

        public void Draw()
        {
            buffer.Graphics.Clear(Color.Goldenrod);
            Player player = null;
            buffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            for (var i = 0; i * 47 < 752; i++)
            {
                for (var j = 0; j * 47 < 752; j++)
                {
                    buffer.Graphics.DrawImage(grassTexture, i * 47, j * 47);
                }
            }
            
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.Type == playerType)
                    player = (Player)gameObject;

                if (gameObject.Type == wallType || gameObject.Type == stationType || gameObject.Type == ghostType)
                    buffer.Graphics.DrawImage(textures[gameObject.Type], gameObject.Position.X, gameObject.Position.Y);

                if (gameObject.Type == bombType)
                    DrawBomb((Bomb)gameObject);
            }

            if (!(player is null))
                buffer.Graphics.DrawImage(textures[player.GetType()], player.Position.X, player.Position.Y);

            buffer.Render();
        }

        private void DrawBomb(Bomb bomb)
        {

            if (bomb.LifeTimer > 6)
                buffer.Graphics.DrawImage(textures[bomb.Type], bomb.Position.X, bomb.Position.Y);
            else
            {
                var radius = Bomb.Radius - bomb.LifeTimer * 10;
                buffer
                    .Graphics
                    .FillEllipse(new SolidBrush(Color.OrangeRed),
                        new RectangleF(bomb.Position.X + bomb.Width / 2 - radius,
                            bomb.Position.Y + bomb.Height / 2 - radius,
                            2 * radius, 2 * radius));
            }
        }
    }
}
