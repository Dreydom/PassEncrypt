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
    public partial class KeyInput : Form
    {
        private Encrypt encrypt = new Encrypt();
        private string pass;
        private byte errorCounter = 0;
        private Bitmap blink = Properties.Resources.blink;
        private Bitmap eye = Properties.Resources.eye;
        public KeyInput(string pass)
        {
            InitializeComponent();
            encrypt.Path = "pass.txt";
            this.pass = pass;
            AcceptButton = button1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string key = textBox1.Text;
            if (String.IsNullOrEmpty(key))
            {
                label2.Text = "Заполните поле";
            }
            else if (encrypt.CheckPass(pass, key))
            {
                errorCounter = 0;
                AccessGranted accessGranted = new AccessGranted(encrypt.WritePass(pass));
                Form formPassInput = Application.OpenForms[0];
                formPassInput.Hide(); //Прячем первую форму
                Hide(); //Прячем вторую форму
                accessGranted.Show();
            }
            else
            {
                errorCounter++;
                label2.Text = $"Неправильный ввод. Попыток осталось —  {(3 - errorCounter)}";
                if (errorCounter == 3)
                {
                    AccessDenied accessDenied = new AccessDenied();
                    Form formPassInput = Application.OpenForms[0];
                    formPassInput.Hide(); //Прячем первую форму
                    Hide(); //Прячем вторую форму
                    accessDenied.Show();
                }
            }
        }
        /// <summary>
        /// Смещение относительно центра, чтобы показать, что предыдущая форма все еще открыта
        /// </summary>
        private void KeyInput_Load(object sender, EventArgs e)
        {
            Top += 20;
            Left += 20;
        }
        /// <summary>
        /// Смена видимости пароля
        /// </summary>
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
        private void KeyInput_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

       
    }
}
