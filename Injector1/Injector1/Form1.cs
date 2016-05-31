//by Vlasov Andrey (andrew.vlasof@yandex.ru)
using System;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using InjectionLibrary;
using JLibrary.PortableExecutable;

namespace Injector1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
  
            try
            {
                var processId = Process.GetProcessesByName("/*PROCESS_NAME*/")[0].Id;       //process CSGO
                WebClient myWebClient = new WebClient();

                byte[] file = myWebClient.DownloadData("/*FILENAME*/"); //download file
                var injector = InjectionMethod.Create(InjectionMethodType.ManualMap);           //type injection
                
                var hModule = IntPtr.Zero;

                using (var img = new PortableExecutable(file))                                  //make .dll
                   
                hModule = injector.Inject(img, processId);                                      //inject

                if (hModule != IntPtr.Zero)
                {
                    // injection was successful
                }
                else
                {
                    // injection failed
                    if (injector.GetLastError() != null)
                        MessageBox.Show(injector.GetLastError().Message);
                }
            }

            catch { MessageBox.Show("Error"); }
            Application.Exit();
        }
    }
}
