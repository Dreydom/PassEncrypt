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
    public partial class AccessGranted : Form
    {
        private string key;
        public AccessGranted(string key)
        {
            InitializeComponent();
            this.key = key;
            richTextBox1.Text = key;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NewPassInput newPassInput = new NewPassInput(key);
            newPassInput.Show();
            Hide();
        }

        /// <summary>
        /// Закрывает все открытые формы
        /// </summary>
        private void AccessGranted_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
