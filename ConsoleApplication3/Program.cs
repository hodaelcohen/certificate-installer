using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Management;


namespace ConsoleApplication3
{
    class Program
    {
        public static void Main(string[] args)
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string root = @"C:\crt";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            using (var client = new WebClient())
            {
                client.DownloadFile("https://agents.019mobile.co.il/appplugins/crtI", "c:\\crt\\cert.crt");
            }

            Console.WriteLine("Certificate instalation!!\nplease press Enter key to continue and then press yes(the instalation will close all the browsers)");
            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);

            InstallCertificate("c:\\crt\\cert.crt");
            if (File.Exists("c:\\crt\\cert.crt"))
                Console.WriteLine("\nInstalation Complete!");

            else Console.WriteLine("Instalation Aborted!");


                
            }

        private static void InstallCertificate(string cerFileName)
        {
            Process[] AllProcesses = Process.GetProcesses();
            foreach (var process in AllProcesses)
            {
                if (process.MainWindowTitle != "")
                {
                    string s = process.ProcessName.ToLower();
                    if (s == "iexplore" || s == "iexplorer" || s == "chrome" || s == "firefox")
                        process.Kill();
                }
            }
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            store.Add(new X509Certificate2(X509Certificate2.CreateFromCertFile(cerFileName)));
            store.Close();
        }


            

            
            
        }

       
}
