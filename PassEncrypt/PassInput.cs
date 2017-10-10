using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PassEncrypt
{
    public partial class PassInput : Form
    {
        private Encrypt encrypt = new Encrypt();
        private Bitmap blink = Properties.Resources.blink;
        private Bitmap eye = Properties.Resources.eye;
        public PassInput()
        {
            InitializeComponent();
            encrypt.Path = "pass.txt";
            AcceptButton = button1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string pass = textBox1.Text;
            if (String.IsNullOrEmpty(pass))
            {
                label2.Text = "Заполните поле";
            }
            else if (!File.Exists(encrypt.Path)) { 
                AccessGranted accessGranted = new AccessGranted(encrypt.WritePass(pass));
                accessGranted.Show();
                Hide();
            }
            else
            {
                KeyInput keyInput = new KeyInput(pass);
                keyInput.Show();
                button1.Hide();
            }
            
        }
        /// <summary>
        /// Смена видимости пароля
        /// </summary>
        /// 
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image == eye)
            {
                pictureBox1.Image = blink;
                textBox1.UseSystemPasswordChar = true;
            }
            else
            {
                pictureBox1.Image = eye;
                textBox1.UseSystemPasswordChar = false;
            }
        }
        /// <summary>
        /// Закрывает все открытые формы
        /// </summary>
        private void PassInput_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
