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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Net;
using System.Net.Sockets;
using MahApps.Metro.Controls.Dialogs;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class IntroWindow : MetroWindow
    {
        TcpClient client = new TcpClient();
        NetworkStream netstream;
        public string ip = "192.168.1.14";
        public int port = 50002;
        static string password = "";
        public IntroWindow()
        {
            try
            {
                // try to connect
                client.Connect(ip, port);
                InitializeComponent();
            }
            catch (SocketException)
            {
                // inform the client about server's breakdown
                MessageBox.Show("Server is corrently down, try to reconnect in a few minutes.");
                this.Close();
            }
                      
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

        private void CAChangeTextWhite(object sender, MouseEventArgs e)
        {
            CAblock.Foreground = new SolidColorBrush(Colors.White);
        }

        private void CAChangeTextBlack(object sender, MouseEventArgs e)
        {
            CAblock.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void FPChangeTextWhite(object sender, MouseEventArgs e)
        {
            CPblock.Foreground = new SolidColorBrush(Colors.White);
        }

        private void FPChangeTextBlack(object sender, MouseEventArgs e)
        {
            CPblock.Foreground = new SolidColorBrush(Colors.Black);
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



        
        private void OpenCreateAccountWindow(object sender, MouseButtonEventArgs e)
        {
            new CreateAccountWindow(client).ShowDialog();
            
        }

        private void OpenChangePassWindow(object sender, MouseButtonEventArgs e)
        {
            new ChangePassWindow(client).ShowDialog(); 

        }
        private void SubmitDetails(object sender, RoutedEventArgs e)
        {
            // send the new user's info via the -checkvalidation code
            SendAll(string.Format("-SubmitDetails {0} {1}", userbox.Text, passbox.Text));

            // if the respond is "-UserIsVarified", open mail form
            string response = RecieveAll();

            // the buffer returns a 1024 byte massage, search only the first 15
            if (response.Substring(0, 15) == "-UserIsVarified") { new EmailValidationWindow(client).ShowDialog(); this.Close(); }
            else ShowMessage("Message",response);

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
