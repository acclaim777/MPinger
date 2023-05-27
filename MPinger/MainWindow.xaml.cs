using MPinger.View;
using System;
using System.Collections.Generic;
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

        private void BT_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BT_EditList_Click(object sender, RoutedEventArgs e)
        {
            StationList stationList = new StationList();
            stationList.Show();
        }

        private void BT_ReloadList_Click(object sender, RoutedEventArgs e)
        {
            DG_Devices.ItemsSource = MainWindowVM.PingStations;
        }
    }
}
