using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WPF_CopyFileWithProgressBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string DestFileName = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var _source = new FileInfo(txtSource.Text);
            var _destination = new FileInfo(txtDestination.Text+ "/"+DestFileName);
            //Check if the file exists, we will delete it
            if (_destination.Exists)
                _destination.Delete();
            //Create a tast to run copy file
            Task.Run(() =>
            {
                _source.Copyfile(_destination, x => progressBar.Dispatcher.BeginInvoke(new Action(() => { progressBar.Value = x; lblPercent.Content = x.ToString() + "%"; })));
            }).GetAwaiter().OnCompleted(() => progressBar.Dispatcher.BeginInvoke(new Action(() => { progressBar.Value = 100; lblPercent.Content = "100%"; MessageBox.Show("You have successfully copied the file !", "Message", MessageBoxButton.OK, MessageBoxImage.Information); })));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                foreach (string filename in fileDialog.FileNames)
                {
                    txtSource.Text = filename; // lbFiles.Items.Add(Path.GetFileName(filename));
                    DestFileName = System.IO.Path.GetFileName(filename);
                }
            }
        }
    }
}
