using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmileWei.EmbeddedApp.WinForm
{
    public partial class FormHandle : Form
    {
        public FormHandle()
        {
            InitializeComponent();
        }
        public IntPtr GetHandle()
        {
            return (IntPtr)(handle.Value);
        }
        private void btnEmbed_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
