using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.Providers
{
    class GoodsCollection
    {
        List<Good> goods = new List<Good>();
        public void AddGood(string name, int price, int count, string button)
        {
            goods.Add(new Good(name, price, count, button));
        }
        public void DeleteGood(string name)
        {
            goods.Remove(goods.Find(x => x.Name == name));
        }
        public List<Good> GetGoods()
        {
            return goods;
        }

    }
}
