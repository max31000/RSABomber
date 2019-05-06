using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSABomber
{
    public partial class MenuForm : Form
    {
        private Handler handler;

        public MenuForm(Handler handler)
        {
            this.handler = handler;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            handler.StartGame();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Image.FromFile(@"Images\pressStartButton.png");
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Image.FromFile(@"Images\startButton.png");
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Image.FromFile(@"Images\startButton.png");
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Image.FromFile(@"Images\startButton.png");
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.BackgroundImage = Image.FromFile(@"Images\pressExitButton.png");
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.BackgroundImage = Image.FromFile(@"Images\exitButton.png");
        }
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Image.FromFile(@"Images\exitButton.png");
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e) 
        {
            pictureBox2.BackgroundImage = Image.FromFile(@"Images\exitButton.png");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
