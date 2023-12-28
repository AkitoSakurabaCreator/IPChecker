using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Sockets;

namespace IP_Checker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string Global_IP = "";
        string Local_IP = "";
        private string hostname = "";

        async void Get_IP()
        {

            var httpClient = new HttpClient();
            Global_IP = await httpClient.GetStringAsync("https://api.ipify.org/");
            label1.Text = "Global IP: " + Global_IP;
            progressBar1.Value = 100;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            Global_IP = "";
            Local_IP = "";


            Get_IP();

            progressBar1.Value = 50;

            hostname = Dns.GetHostName();
            IPAddress[] ips = Dns.GetHostAddresses(hostname);
            foreach (IPAddress a in ips)
            {
                if (a.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    Local_IP += a.ToString() + (Environment.NewLine);
                    break;
                }
            }

            progressBar1.Value = 80;

            label2.Text = "Local IP: " + Local_IP;
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(Global_IP);
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(Local_IP);
        }
    }
}
