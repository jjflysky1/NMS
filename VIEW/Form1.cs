using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace VIEW
{
    public partial class Form1 : Form
    {
        ChromiumWebBrowser browser;
        

        public Form1()
        {
            InitializeComponent();
            ///크롬
            browser = new ChromiumWebBrowser("http://192.168.0.190:75");
            this.panel1.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
            browser.LoadingStateChanged += BrowserLoadingStateChanged;

        }
      
        private static void BrowserLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToString("mm:ss") == "00:00")
            {
                browser.Reload();
            }
        }
    }
}
