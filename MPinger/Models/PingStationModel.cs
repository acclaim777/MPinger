using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPinger.Models
{
    public class PingStationModel : INotifyPropertyChanged
    {
        public PingStationModel()
        {
            Name = "";
            iPAddress = "";
            group = "";
            description = "";
        }
        private int id;
        private string? name;
        private string? iPAddress;
        private string? group;
        private bool pingable;
        private string? description;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string? Name
        {
            get => name; set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string? IPAddress
        {
            get => iPAddress; set
            {
                iPAddress = value.Trim();
                OnPropertyChanged("IPAddress");
            }
        }
        public string? Group
        {
            get => group;
            set
            {
                group = value;
                OnPropertyChanged("Group");
            }
        }
        public bool Pingable
        {
            get => pingable; set
            {
                pingable = value;
                OnPropertyChanged("Pingable");
            }
        }
        public string? Description
        {
            get => description; set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                Debug.WriteLine("Property Updated :" + propertyName);
            }
        }
    }

}
