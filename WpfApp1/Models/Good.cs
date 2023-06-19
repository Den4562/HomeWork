using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1.Models
{
    public class Good
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string Button { get; set; }
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
    }
}
