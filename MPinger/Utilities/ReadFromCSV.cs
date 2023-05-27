using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPinger.Utilities
{
    /// <summary>
    /// reads csv files that are splited with comma " ' " and put to list of PingStationModel
    /// first value must be ID, second must be IP and third must be name
    /// </summary>
    public static class ReadFromCSV
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
    }
}
