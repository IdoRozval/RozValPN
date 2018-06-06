using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using System.Net;
using System.Net.Sockets;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for EmailValidationWindow.xaml
    /// </summary>
    public partial class EmailValidationWindow : MetroWindow
    {
        TcpClient client;
        NetworkStream netstream;
        
        public EmailValidationWindow(TcpClient curr_client)
        {
            InitializeComponent();
            client = curr_client;
        }

        private void ValidationGridEnter(object sender, MouseEventArgs e)
        {
            validationGrid.Background = new SolidColorBrush(Colors.Black);

        }
        private void ValidationGridLeave(object sender, MouseEventArgs e)
        {
            validationGrid.Background = new SolidColorBrush(Color.FromRgb(51,56,67));

        }
        private async void ShowHelp(object sender, MouseButtonEventArgs e)
        {
            await this.ShowMessageAsync("User Validation", "An Email containing your accessing code had just been sent to you.");
        }
        private void CodeBoxFocus(object sender, RoutedEventArgs e)
        {
            if (codebox.Text == "Code") codebox.Clear();
        }

        private void CodeBoxLost(object sender, RoutedEventArgs e)
        {
            if (codebox.Text == "") codebox.Text = "Code";
        }
        private async void ProceedDetails(object sender, MouseButtonEventArgs e)
        {
            // send the new user's info via the -checkvalidation code
            SendAll(string.Format("-ValidationCode {0}", codebox.Text));
            string data = RecieveAll();
            
            Console.WriteLine(data);

            if (data == "-GrantAccess")
            {
                this.Close();
                new VpnClient(client).Show();
            }
            else
            {
                await this.ShowMessageAsync("laga", data);
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
            return Encoding.Default.GetString(content).TrimEnd(new char[1] { '\x00' });
        }
    }
}
