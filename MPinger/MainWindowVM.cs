using MPinger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MPinger
{
    public class MainWindowVM
    {
        private static ObservableCollection<PingStationModel> pingStations;

        public static ObservableCollection<PingStationModel> PingStations
        {
            get => pingStations;
            set => pingStations = value;
        }

        public MainWindowVM()
        {
            readPingList();
        }


        #region working with lists
        private void readPingList()
        {
            if (File.Exists("PingStationList.DAT"))
            {
                PingStations = SaveLoad.Load<ObservableCollection<PingStationModel>>("PingStationList.DAT");
            }
            else
            {
                PingStations = new ObservableCollection<PingStationModel>();
            }
        }

        public static void savePingList()
        {
            ObservableCollection<PingStationModel> _savePingStations = new ObservableCollection<PingStationModel>();
            foreach (var item in PingStations)
            {
                item.Pingable = false;
                item.Description = string.Empty;

                _savePingStations.Add(item);
            }

            SaveLoad.Save<ObservableCollection<PingStationModel>>("PingStationList.DAT", _savePingStations);
        }
        #endregion

        #region pinging

        public void PingDevices()
        {
            ResultPing.PingResultStations = new List<PingStationModel>();
            List<Task> listTask = new List<Task>();

            foreach (var device in PingStations)
            {
                Task task = new Task(() => MPing.PingHost(device));
                listTask.Add(task);
            }

            foreach (var task in listTask)
            {
                task.Start();
            }

            Task.WaitAll(listTask.ToArray());

            refreshData();
        }

        private void refreshData()
        {
            //for (int i = 0; i < PingStations.Count; i++)
            //{
            //    PingStations[i].Pingable = ResultPing.PingResultStations.First(item => item.Id.Equals(PingStations[i].Id)).Pingable;
            //}

            foreach (var device in ResultPing.PingResultStations)
            {
                PingStations.First(item => item.Id.Equals(device.Id)).Pingable = device.Pingable;
                PingStations.First(item => item.Id.Equals(device.Id)).Description = device.Description;
            }
        }
        #endregion

        public string getStatus()
        {
            int pingCount = 0;
            foreach (PingStationModel item in PingStations)
            {
                if (item.Pingable)
                {
                    pingCount++;
                }
            }
            string status = $" {pingCount} of {PingStations.Count} devices are pingable.";
            return status;
        }
    }
}
