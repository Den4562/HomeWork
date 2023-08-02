using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetApp_1
{
    public class Client : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string type;
        private string color;
        private int callory;

        public int Id { 
            get { return id; } 
            set { id = value; 
                OnPropertyChanged("Id"); 
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Type {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }
        public string Color {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }
        public int Callory {
            get { return callory; }
            set
            {
               callory = value;
                OnPropertyChanged("Callory");
            }
        }
       

        public Client(int id, string name, string type, string color,int callory)
        {
            Id = id;
            Name = name;
            Type = type;
            Color = color;
            Callory = callory;
            

        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            return $"Id: {Id},Name: {Name}, Type: {Type}, Color: {Color}, Callory: {Callory}";
        }
    }
}
