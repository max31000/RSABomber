using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
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
        private int score;
        private static int cellSize = 47;
        private SoundPlayer bombBoom;

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
            bombBoom = new SoundPlayer("Sounds/Bomb.wav");
        }

        private void LoadTextures()
        {
            grassTexture = new Bitmap(Image.FromFile("Images\\grass.jpg"), cellSize, cellSize);
            textures = new Dictionary<Type, Image>
            {
                {wallType, new Bitmap(Image.FromFile("Images\\wall.png"), new Size(cellSize, cellSize))},
                {playerType, new Bitmap(Image.FromFile("Images\\nelson.png"), new Size(Player.DefaultWidth, Player.DefaultHeight))},
                {stationType, new Bitmap(Image.FromFile("Images/station.png"), cellSize - 8, cellSize - 8)},
                {bombType, new Bitmap(Image.FromFile("Images\\bomb.png"), 
                                      new Size(cellSize - 7, cellSize - 7))},
                {ghostType, new Bitmap(Image.FromFile("Images\\ghost.png"), cellSize - 6, cellSize - 6) }
            };
        }

        public void DrawScore(int score)
        {
            this.score = score;
        }

        public void Draw()
        {
            buffer.Graphics.Clear(Color.Goldenrod);
            Player player = null;
            buffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            buffer.Graphics.FillRectangle(Brushes.GreenYellow, 0, 0, 760, 50);
            buffer.Graphics.DrawString("Score:" + score, new Font("Arial", 26), Brushes.Black, 10, 5);
            buffer.Graphics.DrawString("WASD - MOVE\nSPACE - DROP BOMB",
                                       new Font("Arial", 14),
                                       Brushes.Black, 540, 5);
            buffer.Graphics.DrawString("You are the South African oppositionist. \nDestroy all power stations.", 
                                       new Font("Arial", 12), 
                                       Brushes.Black, 220, 8);

            buffer.Graphics.TranslateTransform(0, 50);
            for (var i = 0; i < 16; i++)
            {
                for (var j = 0; j < 16; j++)
                    buffer.Graphics.DrawImage(grassTexture, i * (cellSize - 1), j * (cellSize - 1));
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
            buffer.Graphics.TranslateTransform(0, -50);
        }

        private void DrawBomb(Bomb bomb)
        {
            if (bomb.LifeTimer > Bomb.BoomTime)
                buffer.Graphics.DrawImage(textures[bomb.Type], bomb.Position.X, bomb.Position.Y);
            else
            {
                if (bomb.LifeTimer == Bomb.BoomTime)
                    bombBoom.Play();
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
