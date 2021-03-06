﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Net.Sockets;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for ActiveServices.xaml
    /// </summary>
    public partial class ActiveServices : MetroWindow
    {
        TcpClient client, ftp_client, print_client;
        public ActiveServices(TcpClient curr_client, TcpClient ftp_client, TcpClient print_client)
        {
            InitializeComponent();
            client = curr_client;
            this.ftp_client = ftp_client;
            this.print_client = print_client;
        }

    }
}
