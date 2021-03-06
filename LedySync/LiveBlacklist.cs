﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedySync
{
    public partial class LiveBlacklist : Form
    {
        public void loadList()
        {
            this.lv_LiveBlackList.Items.Clear();
            LedySyncMain.mut.WaitOne();
            foreach (KeyValuePair<string, DateTime> entry in Program.main.blackList)
            {
                int delay = Program.main.delayInSecBL;
                bool inWl = Program.main.whiteList.Contains(entry.Key);
                if (inWl)
                {
                    delay = Program.main.delayInSecWL;
                }
                if (entry.Value.AddSeconds(delay) > DateTime.Now)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.BackColor = Color.Red;
                    if (inWl)
                    {
                        lvi.BackColor = Color.Green;
                    }
                    lvi.Text = entry.Key.Insert(4, "-").Insert(9, "-");
                    lvi.SubItems.Add(entry.Value.AddSeconds(delay).ToString("h:mm:ss tt"));
                    lv_LiveBlackList.Items.Add(lvi);
                }
            }
            LedySyncMain.mut.ReleaseMutex();
        }

        public LiveBlacklist()
        {
            InitializeComponent();
        }

        private void btn_removeBL_Click(object sender, EventArgs e)
        {
            LedySyncMain.mut.WaitOne();
            foreach (ListViewItem li in lv_LiveBlackList.Items)
            {
                if (li.Checked) {
                    string szFC = li.Text;
                    szFC = szFC.Replace("-", string.Empty);
                    Program.main.blackList.Remove(szFC);
                    Console.WriteLine(szFC);
                }
            }
            loadList();
            LedySyncMain.mut.ReleaseMutex();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
