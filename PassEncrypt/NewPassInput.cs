using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassEncrypt
{
    public partial class NewPassInput : Form
    {
        private string key;
        private string drive;
        private Bitmap blink = Properties.Resources.blink;
        private Bitmap eye = Properties.Resources.eye;
        private Encrypt encrypt;
        public NewPassInput(string key,string drive)
        {
            this.key = key;
            this.drive = drive;
            InitializeComponent();
            AcceptButton = button1;
            encrypt = new Encrypt(drive);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string oldpass = textBox1.Text;
            string newpass1 = textBox2.Text;
            string newpass2 = textBox3.Text;
            if (String.IsNullOrEmpty(oldpass)
                || String.IsNullOrEmpty(newpass1)
                || String.IsNullOrEmpty(newpass2))
            {
                label4.Text = "Заполните поля";
            }
            else if (!encrypt.CheckPass(oldpass, key))
            {
                label4.Text = "Несовпадение старого пароля";

            }
            else if (newpass1 != newpass2)
            {
                label4.Text = "Введенные пароли не совпадают";
            }
            else
            {
                AccessGranted accessGranted = new AccessGranted(encrypt.WritePass(newpass1),drive);
                accessGranted.Show();
                Hide();
            }
        }

        /// <summary>
        /// Закрывает все открытые формы
        /// </summary>
        private void NewPassInput_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image == eye)
            {
                pictureBox1.Image = blink;
                textBox1.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
            }
            else
            {
                pictureBox1.Image = eye;
                textBox1.UseSystemPasswordChar = false;
                textBox2.UseSystemPasswordChar = false;
                textBox3.UseSystemPasswordChar = false;
            }
        }
    }
}
