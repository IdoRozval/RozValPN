using MahApps.Metro.Controls;
using Microsoft.Win32;
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
using System.IO;
using System.Windows.Forms;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for DatabaseWindow.xaml
    /// </summary>
    public partial class DatabaseWindow : MetroWindow
    {
        public string fpath, fname, fsize;
        bool done = false;
        string[] items;

        

        public DatabaseWindow(string data)
        {
            InitializeComponent();

            items = data.Split(new char[] { ',' });

            for (int i = 0; i < items.Length; i++)
            {
                if (i % 2 == 0)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = items[i];
                    item.MouseDoubleClick += SaveChoice;
                    itemlist.Items.Add(item);

                }
            }
        }

        private void SaveChoice(object sender, MouseButtonEventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();              
                dialog.ShowDialog();
                fpath = dialog.SelectedPath;
                fname = (string)(sender as ListBoxItem).Content;
                fsize = items[2 * itemlist.SelectedIndex + 1];
                done = true;
                this.Close();
            }
            catch { }

          
        }

        private void WarnClient(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!done)
            {
                fname = "";
                fpath = "";
            }

        }

    }
}
