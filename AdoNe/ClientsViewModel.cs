using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetApp_1
{
    public class ClientsViewModel
    {
        public List<Product> Products { get; set; }
        public List<Prodazhi> Prodazhis { get; set; }

        public ClientsViewModel()
        {
            Products = new List<Product>();
            Prodazhis = new List<Prodazhi>();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }

        public void ClearProduct()
        {
            Products.Clear();
        }

        public void AddProdazhi(Prodazhi prodazhi)
        {
            Prodazhis.Add(prodazhi);
        }

        public void RemoveProdazhi(Prodazhi prodazhi)
        {
            Prodazhis.Remove(prodazhi);
        }

        public void ClearProdazhis()
        {
            Prodazhis.Clear();
        }
    }
}
