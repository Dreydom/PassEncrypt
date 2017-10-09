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
        private Encrypt objKeyInput = new Encrypt();
        private string pass;
        private byte errorCounter = 0;
        public KeyInput(string pass)
        {
            InitializeComponent();
            objKeyInput.Path = "pass.txt";
            this.pass = pass;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string key = textBox1.Text;
            if (objKeyInput.CheckPass(pass, key))
            {
                AccessGranted objAccessGranted = new AccessGranted(objKeyInput.WritePass(pass));
                Form PassInput = Application.OpenForms[0];
                PassInput.Hide(); //Прячем первую форму
                Hide(); //Прячем вторую форму
                objAccessGranted.Show();
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
