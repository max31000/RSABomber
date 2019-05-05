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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            handler.StartGame();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            handler.StartGame();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) //кнопка в момент нажатия ЛКМ
        {
            pictureBox1.BackgroundImage = Image.FromFile(@"Images\pressStartButton.png");
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) //кнопка в момент отпускания ЛКМ
        {
            pictureBox1.BackgroundImage = Image.FromFile(@"Images\startButton.png");
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e) // кнопка при наведении на нее курсора
        {
            pictureBox1.BackgroundImage = Image.FromFile(@"Images\startButton.png");
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e) // кнопка когда курсор отведен
        {
            pictureBox1.BackgroundImage = Image.FromFile(@"Images\startButton.png");
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e) //кнопка в момент нажатия ЛКМ
        {
            pictureBox2.BackgroundImage = Image.FromFile(@"Images\pressExitButton.png");
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e) //кнопка в момент отпускания ЛКМ
        {
            pictureBox2.BackgroundImage = Image.FromFile(@"Images\exitButton.png");
        }
        private void pictureBox2_MouseEnter(object sender, EventArgs e) // кнопка при наведении на нее курсора
        {
            pictureBox2.BackgroundImage = Image.FromFile(@"Images\exitButton.png");
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e) // кнопка когда курсор отведен
        {
            pictureBox2.BackgroundImage = Image.FromFile(@"Images\exitButton.png");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
