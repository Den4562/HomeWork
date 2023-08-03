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
    public class Product : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string type;
        private int amount;
        private string manager;
        private int sobivartist;


        public int Id
        {
            get { return id; }
            set
            {
                id = value;
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

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }
        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public string Manager
        {
            get { return manager; }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        public int Sobivartist
        {
            get { return sobivartist; }
            set
            {
                sobivartist = value;
                OnPropertyChanged("Sobivartist");
            }
        }



        public Product(int id, string name, string type, int amount, string manager, int sobivartist)
        {
            Id = id;
            Name = name;
            Type = type;
            Amount = amount;
            Manager = manager;
            Sobivartist= sobivartist;


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            return $"Id: {Id},Name: {Name}, Type: {Type}, Amount: {Amount}, Manager: {Manager}, Sobivartist: {Sobivartist}";
        }
    }
    public class Prodazhi : INotifyPropertyChanged
    {
        private int id;
        private int productId;
        private string name;
        private string manager;
        private int amountSale;
        private int costOne;
        private DateTime dataSell;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public int ProductId
        {
            get { return productId; }
            set
            {
                productId = value;
                OnPropertyChanged("ProductId");
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

        public string Manager
        {
            get { return manager; }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        public int AmountSale
        {
            get { return amountSale; }
            set
            {
                amountSale = value;
                OnPropertyChanged("AmountSale");
            }
        }

        public int CostOne
        {
            get { return costOne; }
            set
            {
                costOne = value;
                OnPropertyChanged("CostOne");
            }
        }

        public DateTime DataSell
        {
            get { return dataSell; }
            set
            {
                dataSell = value;
                OnPropertyChanged("DataSell");
            }
        }

        public Prodazhi(int id, int productId, string name, string manager, int amountSale, int costOne, DateTime dataSell)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            Manager = manager;
            AmountSale = amountSale;
            CostOne = costOne;
            DataSell = dataSell;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            return $"Id: {Id}, ProductId: {ProductId}, Name: {Name}, Manager: {Manager}, AmountSale: {AmountSale}, CostOne: {CostOne}, DataSell: {DataSell}";
        }
    }
}
