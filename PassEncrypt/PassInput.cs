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
        private Encrypt objPassInput = new Encrypt();
        public PassInput()
        {
            InitializeComponent();
            objPassInput.Path = "pass.txt";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(objPassInput.Path)) { 
                AccessGranted objAccessGranted = new AccessGranted(objPassInput.WritePass(textBox1.Text));
                objAccessGranted.Show();
            }
            else
            {
                string pass = textBox1.Text;
                KeyInput objKeyInput = new KeyInput(pass);
                objKeyInput.Show();
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
