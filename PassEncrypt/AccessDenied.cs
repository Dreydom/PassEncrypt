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
    public partial class AccessDenied : Form
    {
        public AccessDenied()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Закрывает все открытые формы
        /// </summary>
        private void AccessDenied_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
