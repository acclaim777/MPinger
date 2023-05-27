using Microsoft.Win32;
using MPinger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace MPinger.View
{
    /// <summary>
    /// Interaction logic for StationList.xaml
    /// </summary>
    public partial class StationList : Window
    {
        public StationList()
        {
            InitializeComponent();
        }

        private void BT_SaveList_Click(object sender, RoutedEventArgs e)
        {
            MainWindowVM.savePingList();
        }

        private void BT_LoadList_Click(object sender, RoutedEventArgs e)
        {
            DG_stationList.ItemsSource = MainWindowVM.PingStations;
        }

        private void BT_ImportFromCSV_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV UTF-8 (Comma delimated) (*.csv)|*.csv";
            openFileDialog.Title = "Open CSV file to import";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                MainWindowVM.PingStations = new ObservableCollection<PingStationModel>();

                ObservableCollection < PingStationModel > devices = ReadFromCSV.ReadCSV(openFileDialog.FileName);

                foreach (var device in devices)
                {
                    MainWindowVM.PingStations.Add(device);
                }

            }
        }
    }
}
