using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedySync
{
    static class Program
    {
        public static BanList bl;
        public static WhiteList wl;
        public static LiveBlacklist blackL;
        public static LedySyncMain main;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            main = new LedySyncMain();
            bl = new BanList();
            blackL = new LiveBlacklist();
            wl = new WhiteList();
            Application.Run(main);
        }
    }
}
