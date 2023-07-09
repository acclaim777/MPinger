using MPinger.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MPinger.Utilities
{
    public static class SaveLoad
    {
        /// <summary>
        /// Generic Load System based on XML Deserializer
        /// </summary>
        /// <typeparam name="T">your model</typeparam>
        /// <param name="FileName">file name for load</param>
        /// <returns></returns>
        public static T Load<T>(string FileName)
        {
            Object rslt;
            if (File.Exists(FileName))
            {
                var xs = new XmlSerializer(typeof(T));
                using (var sr = new StreamReader(FileName))
                {
                    rslt = (T)xs.Deserialize(sr);
                }
                return (T)rslt;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Generic Save System based on XML Serializer
        /// </summary>
        /// <typeparam name="T">your model</typeparam>
        /// <param name="FileName">file name for save</param>
        /// <param name="Obj">actual object based on model</param>
        /// <returns></returns>
        public static bool Save<T>(string FileName, Object Obj)
        {
            var xs = new XmlSerializer(typeof(T));
            using (TextWriter sw = new StreamWriter(FileName))
            {
                xs.Serialize(sw, Obj);
            }
            if (File.Exists(FileName))
                return true;
            else return false;
        }

        /// <summary>
        /// Load ping Stations
        /// </summary>
        /// <param name="FileName">file name for load</param>
        /// <returns></returns>
        public static ObservableCollection<PingStationModel> LoadPingStations(string FileName)
        {
            ObservableCollection<PingStationModel> PingStations;
            if (File.Exists(FileName))
            {
                PingStations = SaveLoad.Load<ObservableCollection<PingStationModel>>(FileName);
            }
            else
            {
                PingStations = new ObservableCollection<PingStationModel>();
            }
            return PingStations;
        }

        /// <summary>
        /// Save ping Stations
        /// </summary>
        /// <param name="FileName">file name for save</param>
        /// <param name="pingStations">actual object</param>
        public static void SavePingStations(string FileName, ObservableCollection<PingStationModel> pingStations)
        {
            ObservableCollection<PingStationModel> _savePingStations = new ObservableCollection<PingStationModel>();
            foreach (var item in pingStations)
            {
                item.Pingable = false;
                item.Description = string.Empty;

                _savePingStations.Add(item);
            }

            SaveLoad.Save<ObservableCollection<PingStationModel>>(FileName, _savePingStations);

        }

    }
}
