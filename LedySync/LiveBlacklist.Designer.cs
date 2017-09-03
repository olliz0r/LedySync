namespace LedySync
{
    partial class LiveBlacklist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveBlacklist));
            this.btn_removeBL = new System.Windows.Forms.Button();
            this.lv_LiveBlackList = new System.Windows.Forms.ListView();
            this.bl_friendCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bl_until = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_removeBL
            // 
            this.btn_removeBL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_removeBL.Location = new System.Drawing.Point(443, 596);
            this.btn_removeBL.Name = "btn_removeBL";
            this.btn_removeBL.Size = new System.Drawing.Size(75, 23);
            this.btn_removeBL.TabIndex = 0;
            this.btn_removeBL.Text = "Remove";
            this.btn_removeBL.UseVisualStyleBackColor = true;
            this.btn_removeBL.Click += new System.EventHandler(this.btn_removeBL_Click);
            // 
            // lv_LiveBlackList
            // 
            this.lv_LiveBlackList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_LiveBlackList.CheckBoxes = true;
            this.lv_LiveBlackList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.bl_friendCode,
            this.bl_until});
            this.lv_LiveBlackList.FullRowSelect = true;
            this.lv_LiveBlackList.Location = new System.Drawing.Point(13, 13);
            this.lv_LiveBlackList.MultiSelect = false;
            this.lv_LiveBlackList.Name = "lv_LiveBlackList";
            this.lv_LiveBlackList.Size = new System.Drawing.Size(587, 577);
            this.lv_LiveBlackList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lv_LiveBlackList.TabIndex = 1;
            this.lv_LiveBlackList.UseCompatibleStateImageBehavior = false;
            this.lv_LiveBlackList.View = System.Windows.Forms.View.Details;
            // 
            // bl_friendCode
            // 
            this.bl_friendCode.Text = "FC";
            this.bl_friendCode.Width = 201;
            // 
            // bl_until
            // 
            this.bl_until.Text = "Blacklisted Until";
            this.bl_until.Width = 198;
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Location = new System.Drawing.Point(524, 596);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 2;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // LiveBlacklist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 631);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.lv_LiveBlackList);
            this.Controls.Add(this.btn_removeBL);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LiveBlacklist";
            this.Text = "LiveBlacklist";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_removeBL;
        private System.Windows.Forms.ListView lv_LiveBlackList;
        private System.Windows.Forms.ColumnHeader bl_friendCode;
        private System.Windows.Forms.ColumnHeader bl_until;
        private System.Windows.Forms.Button btn_Close;
    }
}