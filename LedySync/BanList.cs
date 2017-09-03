using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class BanList : Form
    {

        private void btn_accept_Click(object sender, EventArgs e)
        {
            saveDetails();
            this.Close();
        }
        public BanList()
        {
            InitializeComponent();
            loadDetails();
        }

        public void loadDetails()
        {
            if (File.Exists(Application.StartupPath + "\\banlist.txt"))
            {
                tb_List.Text = File.ReadAllText(Application.StartupPath + "\\banlist.txt");
            }

            string[] lines = tb_List.Lines;

            for(int i=0; i < lines.Length; i++)
            {
                if (lines[i].Length >= 12)
                {
                    Program.main.bannedFCs.Add(lines[i].Substring(0, 12)); //removing newline?
                }
            }
        }

        private void saveDetails()
        {
            File.WriteAllText(Application.StartupPath + "\\banlist.txt", tb_List.Text);
        }
    }
}
