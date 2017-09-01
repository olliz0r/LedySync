using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;

namespace WindowsFormsApplication1
{
    public partial class LedySyncMain : Form
    {
        public LedySyncMain()
        {
            InitializeComponent();
        }
        private TcpListener tcpListener;
        private Thread listenThread;

        private static Mutex mut = new Mutex();

        private Dictionary<string, DateTime> blackList = new Dictionary<string, DateTime>();
        public ArrayList bannedFCs = new ArrayList();



        private void button1_Click(object sender, EventArgs e)
        {
            btn_start.Enabled = false;
            btn_stop.Enabled = true;
            this.tcpListener = new TcpListener(IPAddress.Any, 3000);
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
                            if (blackList[friendCode].AddMinutes(2.0) < DateTime.Now)
                            {
                                blackList[friendCode] = DateTime.Now;
                                retBuffer[0] = 0x01;
                                AppendListViewItem(szConsole, friendCode, szTrainer, szCountry, szRegion, szPokemon);
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
                            AppendListViewItem(szConsole, friendCode, szTrainer, szCountry, szRegion, szPokemon);
                        }
                    }
                    mut.ReleaseMutex();
                    clientStream.Write(retBuffer, 0, retBuffer.Length);
                    clientStream.Flush();
                }
            }

            tcpClient.Close();
        }

        public void AppendListViewItem(string console, string FC, string trainer, string country, string region, string pokemon)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, string, string, string, string, string>(AppendListViewItem), new object[] { console, FC, trainer, country, region, pokemon});
                return;
            }
            string[] row = { DateTime.Now.ToString("h:mm:ss"), console, FC.Insert(4, "-").Insert(9, "-"), trainer, country, region, pokemon };
            var listViewItem = new ListViewItem(row);

            listView1.Items.Add(listViewItem);
            listView1.Items[listView1.Items.Count - 1].EnsureVisible();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            tcpListener.Stop();
            listenThread.Join();
            btn_start.Enabled = true;
            btn_stop.Enabled = false;
        }

        private void btn_banlist_Click(object sender, EventArgs e)
        {
            Program.bl.ShowDialog();
        }
    }
}
