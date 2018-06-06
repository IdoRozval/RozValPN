using System;
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
using System.Timers;
using System.IO;
using System.Net.Sockets;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using MahApps.Metro.Controls.Dialogs;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for VpnClient.xaml
    /// </summary>
    public partial class VpnClient : MetroWindow
    {
        TcpClient client;
        TcpClient ftp_client = new TcpClient(), print_client = new TcpClient();
        NetworkStream netstream;
        int i;
        bool c;


        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        bool settingsReturn, refreshReturn;


        public VpnClient(TcpClient curr_client)
        {
            InitializeComponent();

            // take the handle of the main client
            client = curr_client;

            // sets the initial time zone of machine
            SetTime();

            // tries to connect to the ftp, printer service
            //TryConnectServices();
            

        }
        private void SetTime()
        {
            string time = string.Format("{0:HH:mm:ss tt}", DateTime.Now);
            clockbox.Text = time;
        }
        /*
        private void TryConnectServices()
        {
            
            try
            {
                ftp_client.Connect(ip)
            }
        */
        private void SettingsGridEnter(object sender, MouseEventArgs e)
        {
            settingsimage.RenderTransform = new RotateTransform(30);
            TurnTextWhite(settingstext);
            (sender as Grid).Background = new SolidColorBrush(Colors.Black);
        }
        private void SettingsGridLeave(object sender, MouseEventArgs e)
        {
            settingsimage.RenderTransform = new RotateTransform(0);
            TurnTextGray(settingstext);
            (sender as Grid).Background = new SolidColorBrush(Color.FromRgb(51, 56, 67));

        }
        private void InternetGridEnter(object sender, MouseEventArgs e)
        {
            internetimage.Source = new BitmapImage(new Uri("pack://application:,,,/pictures/webicon2.png"));
            TurnTextWhite(internettext);
            (sender as Grid).Background = new SolidColorBrush(Colors.Black);
        }
        private void InternetGridLeave(object sender, MouseEventArgs e)
        {
            internetimage.Source = new BitmapImage(new Uri("pack://application:,,,/pictures/webicon.png"));
            TurnTextGray(internettext);
            (sender as Grid).Background = new SolidColorBrush(Color.FromRgb(51, 56, 67));
        }
        private void DBGridEnter(object sender, MouseEventArgs e)
        {
            DBimage.Source = new BitmapImage(new Uri("pack://application:,,,/pictures/fileicon2.png"));
            TurnTextWhite(DBtext);
            (sender as Grid).Background = new SolidColorBrush(Colors.Black);
        }
        private void DBGridLeave(object sender, MouseEventArgs e)
        {
            DBimage.Source = new BitmapImage(new Uri("pack://application:,,,/pictures/fileicon.png"));
            TurnTextGray(DBtext);
            (sender as Grid).Background = new SolidColorBrush(Color.FromRgb(51, 56, 67));
        }
        private void PrinterGridEnter(object sender, MouseEventArgs e)
        {
            printerimage.Source = new BitmapImage(new Uri("pack://application:,,,/pictures/printericon2.png"));
            TurnTextWhite(printertext);
            (sender as Grid).Background = new SolidColorBrush(Colors.Black);
        }
        private void PrinterGridLeave(object sender, MouseEventArgs e)
        {
            printerimage.Source = new BitmapImage(new Uri("pack://application:,,,/pictures/printericon.png"));
            TurnTextGray(printertext);
            (sender as Grid).Background = new SolidColorBrush(Color.FromRgb(51, 56, 67));
        }
        private void TurnTextWhite(TextBlock tb)
        {
            tb.Foreground = new SolidColorBrush(Colors.White);
        }
        private void TurnTextGray(TextBlock tb)
        {
            tb.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void ClockLoad(object sender, RoutedEventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
           
            timer.Elapsed += OnTimedEvent;
            timer.Start();

        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try {
                Dispatcher.Invoke(() =>
                {
                    string time = string.Format("{0:HH:mm:ss tt}", DateTime.Now);
                    clockbox.Text = time;

                });
            }
            catch { }
        }

        private void SettingsOpen(object sender, MouseButtonEventArgs e)
        {
            settingsGrid.Visibility = Visibility.Visible;
            internetGrid.Visibility = Visibility.Hidden;
            databaseGrid.Visibility = Visibility.Hidden;
            printerGrid.Visibility = Visibility.Hidden;

        }
        private void InternetOpen(object sender, MouseButtonEventArgs e)
        {
            internetGrid.Visibility = Visibility.Visible;
            databaseGrid.Visibility = Visibility.Hidden;
            settingsGrid.Visibility = Visibility.Hidden;
            printerGrid.Visibility = Visibility.Hidden;

        }
        private void DatabaseOpen(object sender, MouseButtonEventArgs e)
        {
            databaseGrid.Visibility = Visibility.Visible;
            internetGrid.Visibility = Visibility.Hidden;
            settingsGrid.Visibility = Visibility.Hidden;
            printerGrid.Visibility = Visibility.Hidden;

        }
        private void PrinterOpen(object sender, MouseButtonEventArgs e)
        {
            printerGrid.Visibility = Visibility.Visible;
            databaseGrid.Visibility = Visibility.Hidden;
            internetGrid.Visibility = Visibility.Hidden;
            settingsGrid.Visibility = Visibility.Hidden;

        }
 
        
        private void ShowDatabase(object sender, RoutedEventArgs e)
        {
            i = 0;
            c = true;
         
            SendAll("#s");

            DatabaseWindow dbWindow = new DatabaseWindow(RecieveAll());
            dbWindow.ShowDialog();

            if (dbWindow.fpath != "" && dbWindow.fname != "")
            {
                try
                {
                    FileStream fs = new FileStream(dbWindow.fpath + @"\" + dbWindow.fname, FileMode.CreateNew, FileAccess.Write);
                    BinaryWriter bw = new BinaryWriter(fs);
        
                    SendAll(string.Format("#d {0}", dbWindow.fname));

                    int file_size = int.Parse(dbWindow.fsize);
                    byte[] content = new byte[file_size];
                    byte[] content_piece = new byte[1024];

                    client.ReceiveTimeout = 500;
                    while (i < file_size)
                    {
                        content_piece = RecieveAllSuper(content);
                        content = UpdateContent(content, content_piece);
                    }
                    bw.Write(content, 0, content.Length);
                    fs.Close();
                    client.ReceiveTimeout = 0;
                    ShowMessage("Info", "The file has downloaded successfully");
                    

                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("sensitive area, access denied.");
                }
            }    
        }
        private async void ShowMessage(string caption, string content)
        {
            await this.ShowMessageAsync(caption, content);
        }
        private void SendAll(string content)
        {
            byte[] content_bytes = Encoding.Default.GetBytes(content);
            netstream = client.GetStream();
            netstream.Write(content_bytes, 0, content_bytes.Length);
            netstream.Flush();
        }
        private string RecieveAll()
        {
            netstream = client.GetStream();
            byte[] content = new byte[1024];
            netstream.Read(content, 0, content.Length);
            netstream.Flush();
            return Encoding.Default.GetString(content);
        }
        private byte[] RecieveAllSuper(byte[] content)
        {

            string data = "", data_piece = "";
            try
            {
                while (data.Length != 1024 && c)
                {

                    netstream = client.GetStream(); // stream - client's data stream
                    byte[] inStream = new byte[1024 - data.Length];
                    int bytesread = netstream.Read(inStream, 0, 1024 - data.Length); // load data to inStream from index 0-1024                   
                    data_piece = ExtractData(inStream, bytesread);
                    data += data_piece;


                }
                return Encoding.Default.GetBytes(data);
            }
            catch (IOException)
            {
                c = false;
                return Encoding.Default.GetBytes(data);

            }
        }
        private string ExtractData(byte[] inStream, int bytesread)
        {
            byte[] real_inStream = new byte[bytesread];
            for (int j = 0; j < bytesread; j++)
                real_inStream[j] = inStream[j];
            return Encoding.Default.GetString(real_inStream);
        }

    

        private byte[] UpdateContent(byte[] content, byte[] content_add)
        {
            for (int j = 0; j < content_add.Length && i < content.Length; j++)
            {
                content[i] = content_add[j];
                i++;
            }
            return content;

        }

        private void ShowUpload(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            
            if (dialog.FileName != "")
            {
                try
                {
                    FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);


                    // send file name                  
                    SendAll(string.Format("#u {0}", dialog.FileName));


                    byte[] content = br.ReadBytes(1024);
                    while (content.Any())
                    {
                        SendAllBytes(content);
                        content = br.ReadBytes(1024);
                    }
                    fs.Close();
                    ShowMessage("Info", "The file has uploaded successfully");
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("sensitive area, access denied.");
                }
            }
        }

        private void SendAllBytes(byte[] bytes)
        {
            netstream = client.GetStream();
            netstream.Write(bytes, 0, bytes.Length);
            netstream.Flush();
        }

        private void ToogleProxy(object sender, RoutedEventArgs e)
        {
            
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            if (myKey != null && (bool)proxyswitch.IsChecked)
            {
                myKey.SetValue("ProxyEnable", "1", RegistryValueKind.DWord);
                myKey.Close();
                settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
                refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
            }
            else if (myKey != null && !(bool)proxyswitch.IsChecked)
            {
                myKey.SetValue("ProxyEnable", "0", RegistryValueKind.DWord);
                myKey.Close();
                settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
                refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);

            }
            
            
        }

        private void OpenActiveServices(object sender, RoutedEventArgs e)
        {
            new ActiveServices(client, ftp_client, print_client).ShowDialog();
        }
        private void OpenPrint(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                try
                {
                    FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);


                    // send file name                  
                    SendAll(string.Format("@u {0}", dialog.FileName));


                    byte[] content = br.ReadBytes(1024);
                    while (content.Any())
                    {
                        SendAllBytes(content);
                        content = br.ReadBytes(1024);
                    }
                    fs.Close();
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("sensitive area, access denied.");
                }
            }
        }
    
        private void OpenPrintQueue(object sender, RoutedEventArgs e)
        {
            SendAll("@s");
            new PrintQueueWindow(this.Left + this.Width, this.Top, RecieveAll(), client).Show();
        }


    }
}
