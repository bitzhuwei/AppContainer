using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace SmileWei.EmbeddedApp
{
    /// <summary>
    /// 可以把其他窗体应用程序嵌入此容器
    /// </summary>
    [ToolboxBitmap(typeof(AppContainer), "AppControl.bmp")]
    public partial class AppContainer : System.Windows.Forms.Panel
    {
        Action<object, EventArgs> appIdleAction = null;
        EventHandler appIdleEvent = null;

        public AppContainer(bool showEmbedResult = false)
        {
            InitializeComponent();
            this.ShowEmbedResult = showEmbedResult;
            appIdleAction = new Action<object, EventArgs>(Application_Idle);
            appIdleEvent = new EventHandler(appIdleAction);
        }

        public AppContainer(IContainer container, bool showEmbedResult = false)
        {
            container.Add(this);
            InitializeComponent();
            this.ShowEmbedResult = showEmbedResult;
            appIdleAction = new Action<object, EventArgs>(Application_Idle);
            appIdleEvent = new EventHandler(appIdleAction);
        }
        /// <summary>
        /// 将属性<code>AppFilename</code>指向的应用程序打开并嵌入此容器
        /// </summary>
        public void Start()
        {
            if (AppProcess != null)
            {
                Stop();
            }

            try
            {
                ProcessStartInfo info = new ProcessStartInfo(this.m_AppFilename);
                info.UseShellExecute = true;
                info.WindowStyle = ProcessWindowStyle.Minimized;
                //info.WindowStyle = ProcessWindowStyle.Hidden;
                AppProcess = System.Diagnostics.Process.Start(info);
                // Wait for process to be created and enter idle condition
                AppProcess.WaitForInputIdle();
                //todo:下面这两句会引发 NullReferenceException 异常，不知道怎么回事                
                //AppProcess.Exited += new EventHandler(AppProcess_Exited);
                //AppProcess.EnableRaisingEvents = true;
                Application.Idle += appIdleEvent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format("{1}{0}{2}{0}{3}"
                    ,Environment.NewLine
                    , "*" + ex.ToString()
                    , "*StackTrace:" + ex.StackTrace
                    ,"*Source:"+ex.Source
                    ), "Failed to load app.");
                if (AppProcess != null)
                {
                    if (!AppProcess.HasExited)
                        AppProcess.Kill();
                    AppProcess = null;
                }
            }
            
        }
        /// <summary>
        /// 确保应用程序嵌入此容器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Idle(object sender, EventArgs e)
        {
            if (this.AppProcess == null || this.AppProcess.HasExited)
            {
                this.AppProcess = null;
                Application.Idle -= appIdleEvent;
                return;
            }
            if (AppProcess.MainWindowHandle == IntPtr.Zero) return;
            //Application.Idle -= appIdleEvent;
            if (EmbedProcess(AppProcess, this))
            { Application.Idle -= appIdleEvent; }
            //ShowWindow(AppProcess.MainWindowHandle, SW_SHOWNORMAL);
            //var parent = GetParent(AppProcess.MainWindowHandle);//你妹，不管用，全是0
            //if (parent == this.Handle)
            //{
            //    Application.Idle -= appIdleEvent;
            //}
        }
        /// <summary>
        /// 应用程序结束运行时要清除这里的标识
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AppProcess_Exited(object sender, EventArgs e)
        {
            AppProcess = null;
        }

        /// <summary>
        /// Close <code>AppFilename</code> 
        /// <para>将属性<code>AppFilename</code>指向的应用程序关闭</para>
        /// </summary>
        public void Stop()
        {
            if (AppProcess != null)// && AppProcess.MainWindowHandle != IntPtr.Zero)
            {
                try
                {
                    if (!AppProcess.HasExited)
                        AppProcess.Kill();
                }
                catch (Exception)
                {
                }
                AppProcess = null;
                embedResult = 0;
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            Stop();
            base.OnHandleDestroyed(e);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            if (AppProcess != null)
            {
                Win32API.MoveWindow(AppProcess.MainWindowHandle, 0, 0, this.Width, this.Height, true);
            }

            base.OnResize(eventargs);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnSizeChanged(e);
        }

        #region 属性
        /// <summary>
        /// Embedded application's process
        /// </summary>
        public Process AppProcess { get; set; }

        /// <summary>
        /// Target app's file name(*.exe)
        /// </summary>
        private string m_AppFilename = "";
        /// <summary>
        /// Target app's file name(*.exe)
        /// </summary>
        [Category("Data")]
        [Description("Target app's file name(*.exe)")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor(typeof(AppFilenameEditor), typeof(UITypeEditor))]
        public string AppFilename
        {
            get
            {
                return m_AppFilename;
            }
            set
            {
                if (value == null || value == m_AppFilename) return;
                var self = Application.ExecutablePath;
                if (value.ToLower() == self.ToLower())
                {
                    MessageBox.Show("Please don't embed yourself！", "SmileWei.EmbeddedApp");
                    return;
                }
                if (!value.ToLower().EndsWith(".exe"))
                {
                    MessageBox.Show("target is not an *.exe！", "SmileWei.EmbeddedApp");
                }
                if (!File.Exists(value))
                {
                    MessageBox.Show("target does not exist！", "SmileWei.EmbeddedApp");
                    return;
                }
                m_AppFilename = value;
            }
        }
        /// <summary>
        /// 标识内嵌程序是否已经启动
        /// </summary>
        public bool IsStarted { get { return (this.AppProcess != null); } }
        
        #endregion 属性

      
        public void EmbedAgain()
        {
            EmbedProcess(AppProcess, this);
        }

        /// <summary>
        /// 如果函数成功，返回值为子窗口的原父窗口句柄；如果函数失败，返回值为NULL。若想获得多错误信息，请调用GetLastError函数。
        /// </summary>
        public int embedResult = 0;
        /// <summary>
        /// 将指定的程序嵌入指定的控件
        /// </summary>
        private bool EmbedProcess(Process app,Control control)
        {
            // Get the main handle
            if (app == null || app.MainWindowHandle == IntPtr.Zero || control == null) return false;

            embedResult = 0;

            try
            {
                // Put it into this container
                embedResult = Win32API.SetParent(app.MainWindowHandle, control.Handle);
            }
            catch (Exception)
            { }
            try
            {
                // Remove border and whatnot               
                Win32API.SetWindowLong(new HandleRef(this, app.MainWindowHandle), Win32API.GWL_STYLE, Win32API.WS_VISIBLE);
            }
            catch (Exception)
            { }
            try
            {
                // Move the window to overlay it on this window
                Win32API.MoveWindow(app.MainWindowHandle, 0, 0, control.Width, control.Height, true);
            }
            catch (Exception)
            { }

            if (ShowEmbedResult)
            {
                var errorString = Win32API.GetLastError();
                MessageBox.Show(errorString);
            }

            return (embedResult != 0);
        }

        /// <summary>
        /// Show a MessageBox to tell whether the embedding is successfully done.
        /// </summary>
        public bool ShowEmbedResult { get; set; }
    }
}
