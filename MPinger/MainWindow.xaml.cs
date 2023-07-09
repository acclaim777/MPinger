using Microsoft.Win32;
using MPinger.Models;
using MPinger.Utilities;
using MPinger.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;
using static MPinger.MainWindowVM;

namespace MPinger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowVM MWVM;

        public MainWindow()
        {
            if (File.Exists("Settings"))
                GlobalVariables.settings = SaveLoad.Load<SettingsModel>("Settings");
            else
                GlobalVariables.settings = new SettingsModel();



            MWVM = new MainWindowVM();

            InitializeComponent();
            DataContext = MWVM;
        }

        private async void BT_Start_Click(object sender, RoutedEventArgs e)
        {
            BT_Start.IsEnabled = false;
            await Task.Run(() => MWVM.PingDevices());
            BT_Start.IsEnabled = true;

            TB_Status.Text = MWVM.getStatus();
        }

        private void BT_EditList_Click(object sender, RoutedEventArgs e)
        {
            StationList stationList = new StationList();
            stationList.Show();
        }

        private void BT_ReloadList_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in MWVM.PingStations)
            {
                item.Pingable = false;
                item.Description = "";
            }
            
            DG_Devices.ItemsSource = MWVM.PingStations;
        }

        private void BT_OpenList_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MPinger List File (*.MPN)|*.mpn";
            openFileDialog.Title = "Open MPN file";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                MWVM.PingStations = SaveLoad.LoadPingStations(openFileDialog.FileName);
                DG_Devices.ItemsSource = MWVM.PingStations;

                GlobalVariables.settings.LastLoadedFile = openFileDialog.FileName;
                SaveLoad.Save<SettingsModel>("Settings", GlobalVariables.settings);
            }


        }

        private void BT_SaveList_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();//, FileMode.Create, FileAccess.Write
            saveFileDialog1.Filter = "MPinger List File (*.MPN)|*.mpn";
            saveFileDialog1.Title = "Save MPN file";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                SaveLoad.SavePingStations(saveFileDialog1.FileName, MWVM.PingStations);

                GlobalVariables.settings.LastLoadedFile = saveFileDialog1.FileName;
                SaveLoad.Save<SettingsModel>("Settings", GlobalVariables.settings);

            }
        }

        private void BT_NewList_Click(object sender, RoutedEventArgs e)
        {
            MWVM.PingStations = new ObservableCollection<PingStationModel>();
            DG_Devices.ItemsSource = MWVM.PingStations;
        }
    }
}
