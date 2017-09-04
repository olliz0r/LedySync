using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedySync
{
    public partial class WhiteList : Form
    {
        public WhiteList()
        {
            InitializeComponent();
            loadDetails();
        }

        private void btn_accept_Click(object sender, EventArgs e)
        {
            saveDetails();
            loadDetails();
            this.Close();
        }


        public void loadDetails()
        {
            if (File.Exists(Application.StartupPath + "\\whitelist.txt"))
            {
                tb_whiteList.Text = File.ReadAllText(Application.StartupPath + "\\whitelist.txt");
            }

            string[] lines = tb_whiteList.Lines;

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("-", string.Empty);
                if (lines[i].Length >= 12)
                {
                    Program.main.whiteList.Add(lines[i].Substring(0, 12)); //removing newline?
                }
            }
        }

        private void saveDetails()
        {
            File.WriteAllText(Application.StartupPath + "\\whitelist.txt", tb_whiteList.Text);
        }
    }
}
