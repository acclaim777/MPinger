using MPinger.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;

namespace MPinger.Utilities
{
    /// <summary>
    /// reads csv files that are splited with comma " ' " and put to list of PingStationModel
    /// first value must be ID, second must be IP and third must be name
    /// </summary>
    public static class CSVHelper
    {
        public static ObservableCollection<PingStationModel> ReadCSV(string csvFilePath)
        {
            StreamReader reader = null;
            ObservableCollection<PingStationModel> listDevices = new ObservableCollection<PingStationModel>();

            if (File.Exists(csvFilePath))
            {
                reader = new StreamReader(File.OpenRead(csvFilePath));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    PingStationModel device = new PingStationModel{ Id = int.Parse(values[0]), IPAddress = values[1].Trim(), Name = values[2], Group = values[3] };
                    
                    listDevices.Add(device);
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }
            return listDevices;

        }

        public static void WriteCSV(ObservableCollection<PingStationModel> pingStations, string filePath)
        {
            var csv = new StringBuilder();

            foreach (var item in pingStations)
            {
                var Id = item.Id.ToString();
                var IPAddress = item.IPAddress.ToString();
                var Name = item.Name.ToString();
                var Group = item.Group.ToString();

                var newLine = string.Format("{0},{1},{2},{3}", Id, IPAddress, Name, Group);
                csv.AppendLine(newLine);
            }


            File.WriteAllText(filePath, csv.ToString());

            File.WriteAllText(filePath, csv.ToString());

        }
    }
}
