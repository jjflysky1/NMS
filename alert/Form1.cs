﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace alert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("SSIM WATCHER 기간이 만료되었습니다.");
            Opacity = 0;
            ShowInTaskbar = false;
            this.Close();
        }
    }
}