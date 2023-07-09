using MPinger.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPinger.Utilities
{
    public static class GlobalVariables
    {
        public static ObservableCollection<PingStationModel> PingStations;
        public static SettingsModel settings;
    }
}
