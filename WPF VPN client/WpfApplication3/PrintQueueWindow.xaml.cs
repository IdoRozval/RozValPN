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
using System.Net.Sockets;
using System.Net;
using MahApps.Metro.Controls.Dialogs;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for PrintQueueWindow.xaml
    /// </summary>
    public partial class PrintQueueWindow : MetroWindow
    {
        double Xpos, Ypos;
        string[] items;
        Timer incrementor = new Timer();
        TcpClient client;
        NetworkStream netstream;
        
        public PrintQueueWindow(double Xpos, double Ypos, string data, TcpClient curr_client)
        {
            InitializeComponent();
            this.Xpos = Xpos;
            this.Ypos = Ypos;
            this.client = curr_client;

            items = data.Split(new char[] { ',' });

            for (int i = 0; i < items.Length; i++)
            {
                if (i % 2 == 0)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = items[i];
                    item.MouseDoubleClick += PrintFile;
                    itemlist.Items.Add(item);

                }
            }

            incrementor.Interval = 33;
            incrementor.Elapsed += Update;
        }

        private void PrintQueueLoad(object sender, RoutedEventArgs e)
        {
            this.Left = this.Xpos;
            this.Top = this.Ypos;
        }
        private void PrintFile(object sender, MouseButtonEventArgs e)
        {
            incrementor.Start(); SendAll(string.Format("@p {0}", ((ListBoxItem)itemlist.SelectedItem).Content)); 
        }
        
        private void Update(object sender, ElapsedEventArgs e)
        {
            
            this.Dispatcher.Invoke(() =>
            {
                pb.Maximum = 200;           
                if (pb.Value == 200)
                {
                    incrementor.Stop();
                    pb.Value = 0;
                    SendAll("@k");
                    ShowMessage("Alert", "Successfully printed file.");             
                }
                else
                    pb.Value += 1;
            });
            
        }
        private void SendAll(string content)
        {
            byte[] content_bytes = Encoding.Default.GetBytes(content);
            netstream = client.GetStream();
            netstream.Write(content_bytes, 0, content_bytes.Length);
            netstream.Flush();
        }
        private async void ShowMessage(string caption, string content)
        {
            await this.ShowMessageAsync(caption, content);
        }
        private async void PrintAll(object sender, MouseButtonEventArgs e)
        {
            int j = itemlist.Items.Count;
            for (int i = 0; i < j; i++)
            {
                System.Threading.Thread.Sleep(1000);
                SendAll(string.Format("@p {0}", ((ListBoxItem)itemlist.Items[0]).Content));               
                System.Threading.Thread.Sleep(8000);
                SendAll("@k");
                itemlist.Items.Remove((ListBoxItem)itemlist.Items[0]);

            }
            queueTitle.Text = string.Format("Completed");

        }
    }
}
