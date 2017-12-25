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
        private Encrypt encrypt;
        private byte errorCounter = 0;
        private Bitmap blink = Properties.Resources.blink;
        private Bitmap eye = Properties.Resources.eye;
        private string drive;

        public PassInput()
        {
            InitializeComponent();
            search_external_drives(comboBox1);    //поиск внешних накопителей
            comboBox1.SelectedIndex = 0;
            AcceptButton = button1;
        }
        private void search_external_drives(ComboBox input) //поиск носителей
        {
            string mydrive;
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady && (d.DriveType == DriveType.Removable))
                {
                    mydrive = d.Name;
                    input.Items.Add(mydrive);
                }
            }
            if (input.Items.Count == 0)
            {
                input.Items.Add("Внешние носители отсутствуют");
                input.SelectedIndex = 0;
            }
        }
        private int PasswordExists()
        {
            drive = "\\\\.\\" + comboBox1.Text.Remove(2);
            byte[] ByteBuffer = new byte[512];//задаем размер буфера
            byte[] temp = new byte[8];
            FileReader fr = new FileReader();
            if (fr.OpenRead(drive)) //вызов для чтения
            {
                int count = fr.Read(ByteBuffer, 512);

                for (int i = 54; i < 62; i++)
                {
                    temp[i - 54] = ByteBuffer[i];
                }
                fr.Close();
                if (temp[0] == 0)//пароль еще не записан на флешке
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return -1;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string pass = textBox1.Text;
            string key = textBox2.Text;
            encrypt = new Encrypt(drive);
            if (String.IsNullOrEmpty(pass))
            {
                label2.Text = "Заполните все поля";
            }
            else if (PasswordExists() == 0) {
                AccessGranted accessGranted = new AccessGranted(encrypt.WritePass(pass),drive);
                accessGranted.Show();
                Hide();
            }
            else if (String.IsNullOrEmpty(key))
            {
                label2.Text = "Заполните все поля";
            }
            else if (encrypt.CheckPass(pass, key))
            {
                errorCounter = 0;
                AccessGranted accessGranted = new AccessGranted(encrypt.WritePass(pass), drive);
                Hide();
                accessGranted.Show();
            }
            else
            {
                errorCounter++;
                label2.Text = $"Неправильный ввод. Попыток осталось —  {(3 - errorCounter)}";
                if (errorCounter == 3)
                {
                    AccessDenied accessDenied = new AccessDenied();
                    Hide(); 
                    accessDenied.Show();
                }
            }
        }
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
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox2.Image == eye)
            {
                pictureBox2.Image = blink;
                textBox2.UseSystemPasswordChar = true;
            }
            else
            {
                pictureBox2.Image = eye;
                textBox2.UseSystemPasswordChar = false;
            }
        }
        private void PassInput_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PasswordExists() == 1)
            {
                textBox2.Enabled = true;
                pictureBox2.Enabled = true;
                button1.Enabled = true;
            }
            else if (PasswordExists() == 0)
            {
                textBox2.Enabled = false;
                pictureBox2.Enabled = false;
                button1.Enabled = true;
            }
            else if (PasswordExists() == -1)
            {
                button1.Enabled = false;
            }
        }
    }
}
