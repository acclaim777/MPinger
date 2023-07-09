using Microsoft.Win32;
using MPinger.Models;
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
        public ObservableCollection<PingStationModel> PingStations
        {
            get => GlobalVariables.PingStations;
            set
            {
                GlobalVariables.PingStations = value;
            }
        }

        public StationList()
        {
            DataContext = this;
            InitializeComponent();
            
        }

        private void BT_ImportFromCSV_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV UTF-8 (Comma delimated) (*.csv)|*.csv";
            openFileDialog.Title = "Open CSV file to import";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                PingStations = new ObservableCollection<PingStationModel>();

                ObservableCollection < PingStationModel > devices = CSVHelper.ReadCSV(openFileDialog.FileName);

                foreach (var device in devices)
                {
                    PingStations.Add(device);
                }
                DG_stationList.ItemsSource = PingStations;
            }
        }

        private void BT_AddStation_Click(object sender, RoutedEventArgs e)
        {
            PingStations.Add(new PingStationModel { Id = PingStations.Count + 1 });
        }

        private void BT_DelStation_Click(object sender, RoutedEventArgs e)
        {
            if (DG_stationList.SelectedIndex>=0)
            {
                PingStations.RemoveAt(DG_stationList.SelectedIndex);
            }
        }

        private void BT_ExportCSV_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV UTF-8 (Comma delimated) (*.csv)|*.csv";
            saveFileDialog.Title = "Save CSV file";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                CSVHelper.WriteCSV(PingStations,saveFileDialog.FileName);
            }
        }
    }
}
