namespace LedySync
{
    partial class WhiteList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhiteList));
            this.btn_accept = new System.Windows.Forms.Button();
            this.tb_whiteList = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_accept
            // 
            this.btn_accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_accept.Location = new System.Drawing.Point(189, 525);
            this.btn_accept.Name = "btn_accept";
            this.btn_accept.Size = new System.Drawing.Size(75, 23);
            this.btn_accept.TabIndex = 0;
            this.btn_accept.Text = "Accept";
            this.btn_accept.UseVisualStyleBackColor = true;
            this.btn_accept.Click += new System.EventHandler(this.btn_accept_Click);
            // 
            // tb_whiteList
            // 
            this.tb_whiteList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_whiteList.Location = new System.Drawing.Point(12, 12);
            this.tb_whiteList.Multiline = true;
            this.tb_whiteList.Name = "tb_whiteList";
            this.tb_whiteList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_whiteList.Size = new System.Drawing.Size(252, 507);
            this.tb_whiteList.TabIndex = 1;
            // 
            // WhiteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 560);
            this.Controls.Add(this.tb_whiteList);
            this.Controls.Add(this.btn_accept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WhiteList";
            this.Text = "WhiteList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_accept;
        private System.Windows.Forms.TextBox tb_whiteList;
    }
}