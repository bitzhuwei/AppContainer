using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class FormEmbededDemo : Form
    {
        public FormEmbededDemo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Form border style: " + this.FormBorderStyle);
        }

        private void FormEmbededDemo_Load(object sender, EventArgs e)
        {
            this.label1.Text = "Form border style: " + this.FormBorderStyle;
        }
    }
}
