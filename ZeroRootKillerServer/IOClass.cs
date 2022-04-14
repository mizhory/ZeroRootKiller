using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroRootKillerServer
{
    internal class IOClass
    {
        public static void ListenServiceAndExecCommand()
        {
            int tik = 0;
            string cmd = "";
            while(tik <= 100)
            {
                cmd = ZeroRootKillerServer.IOClass.ListenCommand();
                if (cmd.Length > 0)
                {
                    ZeroRootKillerServer.IOClass.ExecCommand(cmd);
                }
                tik++;
            }
        }
        public static string ListenCommand()
        {
            string listcommand = "";

            return listcommand;
        }
        private static void ExecCommand(string cmd = "" )
        {
            if (cmd.Length <= 0) return;
            else
            {
                switch (cmd)
                {
                    case "1":
                    break;
                }
            }
        }
    }
}
