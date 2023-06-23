using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.Models
{
    class Good : INotifyPropertyChanged
    {
        protected string name;
        protected int price;
        protected int count;
        protected string button;
        public string Name {
            get { return name; }
            set { 
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Price {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        public int Count { 
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }
        public string Button { 
            get { return button; }
            set { 
                button = value;
                OnPropertyChanged("Button");
            }
        
        }
        public Good(string name, int price, int count, string Button)
        {
            Name = name;
            Price = price;
            Count = count;
            this.Button = Button;
        }
        public override string ToString()
        {
            return Name + " " + Price + " " + Count;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
