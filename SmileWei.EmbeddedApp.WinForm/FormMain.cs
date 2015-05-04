using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SmileWei.EmbeddedApp.WinForm
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.appBox.ShowEmbedResult = true;
            Application.Idle += Application_Idle;
            //appBox.AppFilename = @"C:\Users\DELL\AppData\Local\Google\Chrome\Application\chrome.exe";
            //appBox.Start();
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (appBox.IsStarted)
            {
                if (!appBox.AppProcess.HasExited)
                {
                    try
                    {
                        lblInfo.Text = string.Format("Main Window Handle:{0}|Original Parent Window Handle:{1}",
                            appBox.AppProcess.MainWindowHandle,
                            appBox.embedResult);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            //throw new NotImplementedException();
        }

        private void btnBrowseApp_Click(object sender, EventArgs e)
        {
            if (openApp.ShowDialog(this)== DialogResult.OK)
            {
                appBox.AppFilename = openApp.FileName;
                appBox.Start();
                if (appBox.IsStarted)
                {
                    txtAppFilename.Text = appBox.AppFilename;
                    
                }
            }
        }

        private void lblEmbedAgain_Click(object sender, EventArgs e)
        {
            appBox.EmbedAgain();
            //var embedResult = appBox.embedResult;
            //if(embedResult==0)
            //{
            //    var errorString = AppContainer.GetLastError();
            //    MessageBox.Show(errorString);
            //}
        }

        private void lblEmbedHandle_Click(object sender, EventArgs e)
        {
            var frmHandle = new FormHandle();
            if (frmHandle.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                var handle = frmHandle.GetHandle();
                SetParent(handle, this.Handle);
                Win32API.SetWindowLong(new HandleRef(this.appBox, handle), GWL_STYLE, WS_VISIBLE);       
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        //[DllImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        //private static extern long SetWindowLong(IntPtr hwnd, int nIndex, long dwNewLong);
        [DllImport("user32", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern uint SetWindowLong(
        IntPtr hwnd,
        int nIndex,
        uint dwNewLong
        );
        private void lblInfo_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.lblInfo.Text);
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            var app = appBox.AppProcess;
            if (app == null) { return; }

            var c = Form.FromHandle(app.MainWindowHandle);
            var f = c as Form;
            if (f != null)
            {
                Console.WriteLine(f.Parent == null);
            }

        }

    }
}
