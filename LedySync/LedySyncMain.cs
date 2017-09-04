using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;

namespace LedySync
{
    public partial class LedySyncMain : Form
    {
        public LedySyncMain()
        {
            InitializeComponent();
        }
        private TcpListener tcpListener = null;
        private Thread listenThread;

        public static Mutex mut = new Mutex();

        public Dictionary<string, DateTime> blackList = new Dictionary<string, DateTime>();
        public ArrayList bannedFCs = new ArrayList();
        public ArrayList whiteList = new ArrayList();
        public int delayInSecBL = 600;
        public int delayInSecWL = 60;


        private void button1_Click(object sender, EventArgs e)
        {
            btn_start.Enabled = false;
            btn_stop.Enabled = true;
            tb_port.Enabled = false;
            tb_timeout.Enabled = false;
            tb_whitelist.Enabled = false;
            delayInSecBL = Int32.Parse(tb_timeout.Text);
            delayInSecWL = Int32.Parse(tb_whitelist.Text);
            this.tcpListener = new TcpListener(IPAddress.Any, Int32.Parse(tb_port.Text));
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();
            bool dead = false;
            Thread clientThread = null;
            while (!dead)
            {
                //blocks until a client has connected to the server
                try
                {
                    TcpClient client = this.tcpListener.AcceptTcpClient();
                    //create a thread to handle communication 
                    //with connected client
                    clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(client);
                }catch
                {
                    dead = true;
                    if (clientThread != null)
                    {
                        clientThread.Join();
                    }
                }
            }
        }

        public byte calculateChecksum(byte[] principal)
        {
            byte[] newPrincipal = new byte[4];
            Array.Copy(principal, newPrincipal, 4);
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] hash = sha1.ComputeHash(newPrincipal);
                byte MyChecksum = (byte)(hash[0] >> 1);
                return MyChecksum;
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                if(message[0] == 0x00)
                {
                    byte[] principal = new byte[4];
                    Array.Copy(message, 1, principal, 0, 4);
                    byte[] rest = new byte[4096];

                    Array.Copy(message, 5, rest, 0, 4000);

                    string szRest = Encoding.UTF8.GetString(rest);

                    int icounter = 0;
                    int iStart = 0;
                    string szConsole = "";
                    string szTrainer = "";
                    string szCountry = "";
                    string szRegion = "";
                    string szPokemon = "";
                    string szPage = "";
                    string szIndex = "";
                    for (int i = 0; i < szRest.Length; i++)
                    {
                        if(szRest[i] == '\t')
                        {
                            if (icounter == 0)
                            {
                                szConsole = szRest.Substring(iStart, i - iStart);
                                icounter++;
                                iStart = i + 1;
                            }else if(icounter == 1)
                            {
                                szTrainer = szRest.Substring(iStart, i - iStart);
                                icounter++;
                                iStart = i + 1;
                            }
                            else if (icounter == 2)
                            {
                                szCountry = szRest.Substring(iStart, i - iStart);
                                icounter++;
                                iStart = i + 1;
                            }
                            else if (icounter == 3)
                            {
                                szRegion = szRest.Substring(iStart, i - iStart);
                                icounter++;
                                iStart = i + 1;
                            }
                            else if (icounter == 4)
                            {
                                szPokemon = szRest.Substring(iStart, i - iStart);
                                icounter++;
                                iStart = i + 1;
                            }
                            else if (icounter == 5)
                            {
                                szPage = szRest.Substring(iStart, i - iStart);
                                icounter++;
                                iStart = i + 1;
                            }
                            else if (icounter == 6)
                            {
                                szIndex = szRest.Substring(iStart, i - iStart);
                                icounter++;
                                iStart = i + 1;
                            }
                        }
                    }


                    byte[] fc = new byte[8];
                    byte checksum = calculateChecksum(principal);
                    Array.Copy(principal, 0, fc, 0, 4);
                    fc[4] = checksum;
                    long iFC = BitConverter.ToInt64(fc, 0);
                    string friendCode = iFC.ToString().PadLeft(12, '0');



                    clientStream = tcpClient.GetStream();
                    byte[] retBuffer = { 0x00 };

                    mut.WaitOne();
                    if (bannedFCs.Contains(friendCode))
                    {
                        retBuffer[0] = 0x02;
                    }
                    else
                    {
                        if (blackList.ContainsKey(friendCode))
                        {
                            int delay = delayInSecBL;
                            if (whiteList.Contains(friendCode))
                            {
                                delay = delayInSecWL;
                            }
                            if (blackList[friendCode].AddSeconds(delay) < DateTime.Now)
                            {
                                blackList[friendCode] = DateTime.Now;
                                retBuffer[0] = 0x01;
                                AppendListViewItem(szConsole, friendCode, szTrainer, szCountry, szRegion, szPokemon, szPage, szIndex);
                            }
                            else
                            {
                                retBuffer[0] = 0x00;
                            }
                        }
                        else
                        {
                            blackList.Add(friendCode, DateTime.Now);
                            retBuffer[0] = 0x01;
                            AppendListViewItem(szConsole, friendCode, szTrainer, szCountry, szRegion, szPokemon, szPage, szIndex);
                        }
                    }
                    mut.ReleaseMutex();
                    clientStream.Write(retBuffer, 0, retBuffer.Length);
                    clientStream.Flush();
                }
            }

            tcpClient.Close();
        }

        public void AppendListViewItem(string console, string FC, string trainer, string country, string region, string pokemon, string page, string index)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, string, string, string, string, string, string, string>(AppendListViewItem), new object[] { console, FC, trainer, country, region, pokemon, page, index});
                return;
            }
            string[] row = { DateTime.Now.ToString("h:mm:ss tt"), console, FC.Insert(4, "-").Insert(9, "-"), trainer, country, region, pokemon, page, index };
            var listViewItem = new ListViewItem(row);

            lv_log.Items.Add(listViewItem);
            lv_log.Items[lv_log.Items.Count - 1].EnsureVisible();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            tcpListener.Stop();
            listenThread.Join();
            btn_start.Enabled = true;
            btn_stop.Enabled = false;
            tb_port.Enabled = true;
            tb_timeout.Enabled = true;
            tb_whitelist.Enabled = true;
        }

        private void btn_banlist_Click(object sender, EventArgs e)
        {
            Program.bl.ShowDialog();
        }

        private void LedySyncMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.timeoutBL = tb_timeout.Text;
            Properties.Settings.Default.timeoutWL = tb_whitelist.Text;
            Properties.Settings.Default.Port = tb_port.Text;
            if (tcpListener != null)
            {
                tcpListener.Stop();
            }
            Application.Exit();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            ListViewToCSV(lv_log, AppDomain.CurrentDomain.BaseDirectory + "\\Syncexport.csv", true);
            MessageBox.Show("Exported!");
        }

        public static void ListViewToCSV(ListView listView, string filePath, bool includeHidden)
        {
            //make header string
            StringBuilder result = new StringBuilder();
            WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listView.Columns[i].Text);

            //export data rows
            foreach (ListViewItem listItem in listView.Items)
                WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listItem.SubItems[i].Text);

            File.WriteAllText(filePath, result.ToString());
        }

        private static void WriteCSVRow(StringBuilder result, int itemsCount, Func<int, bool> isColumnNeeded, Func<int, string> columnValue)
        {
            bool isFirstTime = true;
            for (int i = 0; i < itemsCount; i++)
            {
                if (!isColumnNeeded(i))
                    continue;

                if (!isFirstTime)
                    result.Append(",");
                isFirstTime = false;

                result.Append(String.Format("\"{0}\"", columnValue(i)));
            }
            result.AppendLine();
        }

        private void ImportCSV()
        {
            FileStream srcFS;
            srcFS = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Syncexport.csv", FileMode.Open);
            StreamReader srcSR = new StreamReader(srcFS, System.Text.Encoding.Default);
            srcSR.ReadLine();
            do
            {
                string ins = srcSR.ReadLine();
                if (ins != null)
                {
                    string[] columns = ins.Replace("\"", "").Split(',');

                    ListViewItem lvi = new ListViewItem(columns[0]);

                    for (int i = 1; i < columns.Count(); i++)
                    {
                        lvi.SubItems.Add(columns[i]);
                    }

                    lv_log.Items.Add(lvi);
                }
                else break;
            } while (true);
            srcSR.Close();
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            ImportCSV();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            lv_log.Items.Clear();
        }

        private void btn_LiveBL_Click(object sender, EventArgs e)
        {
            Program.blackL.loadList();
            Program.blackL.ShowDialog();
        }

        private void btn_whitelist_Click(object sender, EventArgs e)
        {
            Program.wl.ShowDialog();
        }

        private void LedySyncMain_Load(object sender, EventArgs e)
        {

            tb_timeout.Text = Properties.Settings.Default.timeoutBL;
            tb_whitelist.Text = Properties.Settings.Default.timeoutWL;
            tb_port.Text = Properties.Settings.Default.Port;
        }
    }
}
