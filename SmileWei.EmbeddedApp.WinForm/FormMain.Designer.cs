namespace SmileWei.EmbeddedApp.WinForm
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnBrowseApp = new System.Windows.Forms.Button();
            this.txtAppFilename = new System.Windows.Forms.TextBox();
            this.openApp = new System.Windows.Forms.OpenFileDialog();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.lblEmbedAgain = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEmbedHandle = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.appBox = new SmileWei.EmbeddedApp.AppContainer(this.components);
            this.statusMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowseApp
            // 
            this.btnBrowseApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseApp.Location = new System.Drawing.Point(310, 284);
            this.btnBrowseApp.Name = "btnBrowseApp";
            this.btnBrowseApp.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseApp.TabIndex = 1;
            this.btnBrowseApp.Text = "浏览...";
            this.btnBrowseApp.UseVisualStyleBackColor = true;
            this.btnBrowseApp.Click += new System.EventHandler(this.btnBrowseApp_Click);
            // 
            // txtAppFilename
            // 
            this.txtAppFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppFilename.Location = new System.Drawing.Point(12, 284);
            this.txtAppFilename.Name = "txtAppFilename";
            this.txtAppFilename.ReadOnly = true;
            this.txtAppFilename.Size = new System.Drawing.Size(292, 21);
            this.txtAppFilename.TabIndex = 2;
            // 
            // openApp
            // 
            this.openApp.Filter = "可执行程序 (*.exe)|*.exe";
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEmbedAgain,
            this.lblEmbedHandle,
            this.lblInfo});
            this.statusMain.Location = new System.Drawing.Point(0, 310);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(397, 22);
            this.statusMain.TabIndex = 3;
            this.statusMain.Text = "statusStrip1";
            // 
            // lblEmbedAgain
            // 
            this.lblEmbedAgain.Name = "lblEmbedAgain";
            this.lblEmbedAgain.Size = new System.Drawing.Size(56, 17);
            this.lblEmbedAgain.Text = "再次嵌入";
            this.lblEmbedAgain.Click += new System.EventHandler(this.lblEmbedAgain_Click);
            // 
            // lblEmbedHandle
            // 
            this.lblEmbedHandle.Name = "lblEmbedHandle";
            this.lblEmbedHandle.Size = new System.Drawing.Size(56, 17);
            this.lblEmbedHandle.Text = "句柄嵌入";
            this.lblEmbedHandle.Click += new System.EventHandler(this.lblEmbedHandle_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(47, 17);
            this.lblInfo.Text = "handle";
            this.lblInfo.Click += new System.EventHandler(this.lblInfo_Click);
            // 
            // appBox
            // 
            this.appBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appBox.AppFilename = "";
            this.appBox.AppProcess = null;
            this.appBox.Location = new System.Drawing.Point(12, 12);
            this.appBox.Name = "appBox";
            this.appBox.Size = new System.Drawing.Size(373, 266);
            this.appBox.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 332);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.txtAppFilename);
            this.Controls.Add(this.btnBrowseApp);
            this.Controls.Add(this.appBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "内嵌程序";
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AppContainer appBox;
        private System.Windows.Forms.Button btnBrowseApp;
        private System.Windows.Forms.TextBox txtAppFilename;
        private System.Windows.Forms.OpenFileDialog openApp;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripStatusLabel lblEmbedAgain;
        private System.Windows.Forms.ToolStripStatusLabel lblEmbedHandle;
        private System.Windows.Forms.ToolStripStatusLabel lblInfo;
    }
}

