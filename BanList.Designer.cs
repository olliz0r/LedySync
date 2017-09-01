namespace WindowsFormsApplication1
{
    partial class BanList
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
            this.tb_List = new System.Windows.Forms.TextBox();
            this.btn_accept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_List
            // 
            this.tb_List.Location = new System.Drawing.Point(13, 13);
            this.tb_List.MaxLength = 999999999;
            this.tb_List.Multiline = true;
            this.tb_List.Name = "tb_List";
            this.tb_List.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_List.Size = new System.Drawing.Size(217, 450);
            this.tb_List.TabIndex = 0;
            // 
            // btn_accept
            // 
            this.btn_accept.Location = new System.Drawing.Point(155, 478);
            this.btn_accept.Name = "btn_accept";
            this.btn_accept.Size = new System.Drawing.Size(75, 23);
            this.btn_accept.TabIndex = 1;
            this.btn_accept.Text = "Accept";
            this.btn_accept.UseVisualStyleBackColor = true;
            this.btn_accept.Click += new System.EventHandler(this.btn_accept_Click);
            // 
            // BanList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 513);
            this.Controls.Add(this.btn_accept);
            this.Controls.Add(this.tb_List);
            this.Name = "BanList";
            this.Text = "BanList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_List;
        private System.Windows.Forms.Button btn_accept;
    }
}