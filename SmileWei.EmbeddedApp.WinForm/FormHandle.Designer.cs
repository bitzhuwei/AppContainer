namespace SmileWei.EmbeddedApp.WinForm
{
    partial class FormHandle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHandle));
            this.label1 = new System.Windows.Forms.Label();
            this.handle = new System.Windows.Forms.NumericUpDown();
            this.btnEmbed = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.handle)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "要嵌入的窗体句柄：";
            // 
            // handle
            // 
            this.handle.Location = new System.Drawing.Point(131, 12);
            this.handle.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.handle.Name = "handle";
            this.handle.Size = new System.Drawing.Size(94, 21);
            this.handle.TabIndex = 1;
            // 
            // btnEmbed
            // 
            this.btnEmbed.Location = new System.Drawing.Point(231, 9);
            this.btnEmbed.Name = "btnEmbed";
            this.btnEmbed.Size = new System.Drawing.Size(75, 23);
            this.btnEmbed.TabIndex = 2;
            this.btnEmbed.Text = "嵌入";
            this.btnEmbed.UseVisualStyleBackColor = true;
            this.btnEmbed.Click += new System.EventHandler(this.btnEmbed_Click);
            // 
            // FormHandle
            // 
            this.AcceptButton = this.btnEmbed;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 49);
            this.Controls.Add(this.btnEmbed);
            this.Controls.Add(this.handle);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormHandle";
            this.Text = "根据句柄嵌入窗体";
            ((System.ComponentModel.ISupportInitialize)(this.handle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown handle;
        private System.Windows.Forms.Button btnEmbed;
    }
}