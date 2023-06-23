using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.Providers
{
    class GoodsCollection: INotifyPropertyChanged
    {
        private List<Good> goods = new List<Good>();

        private Good selectedGood;
        public Good SelectedGood
        {
            get { return selectedGood; }
            set
            {
                selectedGood = value;
                OnPropertyChanged("SelectedGood");
            }
        }
        public List<Good> Goods
        {
            get { return goods; }
            set
            {
                goods = value;
                OnPropertyChanged("Goods");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void AddGood(string name, int price, int count, string button)
        {
            goods.Add(new Good(name, price, count, button));
        }
        public void DeleteGood(string name)
        {
            goods.Remove(goods.Find(x => x.Name == name));
        }
    }
}
