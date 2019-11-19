using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ChangePulseDNS
{
    class Program
    {
        static void Main(string[] args)
        {

            string connection_string = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\lssrvifr01\d$\SqlData\SUNITALY_data.mdfp;Persist Security Info=False";
            //using (OleDbConnection cn = new OleDbConnection(connection_string))
            //{
            //    cn.Open();
            //    cn.Close();
            //}

                string myHost = Dns.GetHostName();
            List<IPAddress> myIP = Dns.GetHostEntry(myHost).AddressList.ToList();
            List<string> WiFiipAddrName = GetAllLocalIPv4(NetworkInterfaceType.Wireless80211);
            List<string> ipAddrName = GetAllLocalIPv4(NetworkInterfaceType.Ethernet);


            //Eseguo il netsh con i privilegi da amministratore
            foreach (string ip in ipAddrName)
            {
                ProcessStartInfo psi2 = new ProcessStartInfo("netsh", "interface ip set dns name=\"" + ip + "\" source=dhcp");
                psi2.UseShellExecute = true;
                psi2.Verb = "runas";
                Process.Start(psi2);
                Console.WriteLine("Obtain Success");

            }
            foreach (string ip in WiFiipAddrName)
            {
                ProcessStartInfo psi2 = new ProcessStartInfo("netsh", "interface ip set dns name=\"" + ip + "\" source=dhcp");
                psi2.UseShellExecute = true;
                psi2.Verb = "runas";
                Process.Start(psi2);
                Console.WriteLine("Obtain Success");

            }

            //var secure = new SecureString();
            ////var s = "Alliance10".Aggregate(new SecureString(), (ss, c) => { ss.AppendChar(c); return secure; });
            //foreach (char c in "Alliance10".ToCharArray())
            //{
            //    secure.AppendChar(c);
            //}
            //var psi = new ProcessStartInfo
            //{
            //    FileName = @"C:\Users\llamalfa\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Accessories\iexplore.exe",
            //    //Arguments = "interface ip set dns name=\"" + ip + "\" source=dhcp",
            //    UserName = "localsupport",
            //    Domain = @"alliance-eu.dom\",
            //    PasswordInClearText = "Alliance10",
            //    UseShellExecute = false,
            //    RedirectStandardOutput = true,
            //    RedirectStandardError = true,
            //    Verb = "runas"

            //};
            //Process.Start(psi);

            //    var psi = new ProcessStartInfo("cmd")
            //    {
            //        UseShellExecute = false,
            //        UserName = "localsupport"
            //    };

            //    SecureString password = new SecureString();

            //    Console.WriteLine("Please type in the password for 'username':");
            //    var readLine = Console.ReadLine(); // this should be masked in some way.. ;)

            //    if (readLine != null)
            //    {
            //        foreach (var character in readLine)
            //        {
            //            password.AppendChar(character);
            //        }

            //        psi.Password = password;
            //    }

            //    var ps = Process.Start(psi);






        }


        //Metodo per recuperare gli ip locali
        public static List<string> GetAllLocalIPv4(NetworkInterfaceType _type)
        {
            List<string> ipAddrList = new List<string>();
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    ipAddrList.Add(item.Name);
                }
            }
            return ipAddrList;
        }
    }
}
