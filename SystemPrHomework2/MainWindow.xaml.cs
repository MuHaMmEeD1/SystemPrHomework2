using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace SystemPrHomework2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Thread CopyThread;
        bool Icaze = false;
        bool YoxlaMod = false;

        public string inStr { get; set; } = "";
        public string outStr { get; set; } = "";

        public MainWindow()
        {
            InitializeComponent();

            CopyThread = new Thread(() =>
            {
                string metin = File.ReadAllText(inStr);


                for (int i = 0; i < metin.Length; i++)
                {
                    File.AppendAllText(outStr, metin[i].ToString());
                    Dispatcher.Invoke(() => PR_Bar.Value = i);

                    Thread.Sleep(50);
                }

                 Icaze = false;
                 YoxlaMod = false;

                 inStr = "";
                 outStr = "";
                Dispatcher.Invoke(() => GelenTextBox.Text = "");
                Dispatcher.Invoke(() => GeDenTextBox.Text = "");
                Dispatcher.Invoke(() => PR_Bar.Value = 0);

            });







        }

        private void GelenFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Icaze)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "txt|*.txt";
                openFileDialog.FilterIndex = 1;

                var netice = openFileDialog.ShowDialog();

                if (netice.HasValue && netice.Value)
                {

                    this.GelenTextBox.Text = openFileDialog.FileName;
                    inStr = openFileDialog.FileName;

                }
            }
            else { MessageBox.Show("Buna Icaze Yoxdu Hal Hazirda File Endrilir"); }
        }

        private void GeDenFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Icaze)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "txt|*.txt";
                openFileDialog.FilterIndex = 1;

                var netice = openFileDialog.ShowDialog();

                if (netice.HasValue && netice.Value)
                {

                    this.GeDenTextBox.Text = openFileDialog.FileName;
                    outStr = openFileDialog.FileName;
                }
            }
            else { MessageBox.Show("Buna Icaze Yoxdu Hal Hazirda File Endrilir"); }

        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Icaze && inStr!="" && outStr!="")
            {
                CopyThread.Start();
                string metin = File.ReadAllText(inStr);
                PR_Bar.Maximum = metin.Length;
                Icaze = true;
                YoxlaMod = true;
            }
            else if (!Icaze && inStr == "" && outStr == "") { MessageBox.Show("Buna Icaze Yoxdu Hal Hazirda File Yoxdu"); }
            else { MessageBox.Show("Buna Icaze Yoxdu Hal Hazirda File Endrilir"); }

        }

        private void SuspendButton_Click(object sender, RoutedEventArgs e)
        {
            if (Icaze)
            {
                CopyThread.Suspend();
                YoxlaMod = false;
            }
            else { MessageBox.Show("Buna Icaze Yoxdu Hal Hazirda Copy Start Olunmayib"); }

        }

        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Icaze)
            {
                CopyThread.Resume();
                YoxlaMod = true;
            }
            else { MessageBox.Show("Buna Icaze Yoxdu Hal Hazirda Copy Start Olunmayib"); }

        }

        private void AbortButton_Click(object sender, RoutedEventArgs e)
        {
            if (Icaze && YoxlaMod)
            {
                CopyThread.Abort();
                PR_Bar.Value = 0;
                Icaze = false;
            }
            else if (!YoxlaMod && Icaze) { MessageBox.Show("Buna Icaze Yoxdu Hal Hazirda Suspen Olduqu Ucun"); }
            else { MessageBox.Show("Buna Icaze Yoxdu Hal Hazirda Copy Start Olunmayib"); }

        }

    }
}
