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
using System.Net.Sockets;
using MahApps.Metro.Controls.Dialogs;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for CreateAccountWindow.xaml
    /// </summary>
    public partial class CreateAccountWindow : MetroWindow
    {
        NetworkStream netstream;
        TcpClient client;
        public CreateAccountWindow(TcpClient curr_client)
        {
            InitializeComponent();
            client = curr_client;
        }
        private void UserBoxFocus(object sender, RoutedEventArgs e)
        {
            if (userbox.Text == "Username") userbox.Clear();
        }
        private void UserBoxLost(object sender, RoutedEventArgs e)
        {
            if (userbox.Text == "") userbox.Text = "Username";
        }

        private void PassBoxFocus(object sender, RoutedEventArgs e)
        {
            if (passbox.Text == "Password") passbox.Clear();
        }

        private void PassBoxLost(object sender, RoutedEventArgs e)
        {
            if (passbox.Text == "") passbox.Text = "Password";
        }
        private void EmailBoxFocus(object sender, RoutedEventArgs e)
        {
            if (emailbox.Text == "Email") emailbox.Clear();
        }

        private void EmailBoxLost(object sender, RoutedEventArgs e)
        {
            if (emailbox.Text == "") emailbox.Text = "Email";
        }
        private void ButtonColorWhite(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            b.Foreground = new SolidColorBrush(Colors.White);
        }
        private void ButtonColorBlack(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            b.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void SendDetails(object sender, RoutedEventArgs e)
        {
            // send the new user's info via the -checkvalidation code
            SendAll(string.Format("-CheckValidation {0} {1} {2}", userbox.Text, passbox.Text, emailbox.Text));
            ShowMessage("Server Respond", RecieveAll().TrimEnd(new char[1] { '\x00' }));

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
    }
}
