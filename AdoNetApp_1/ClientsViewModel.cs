using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetApp_1
{
    public class ClientsViewModel
    {
        public List<Client> Clients { get; set; }
        public ClientsViewModel()
        {
            Clients = new List<Client>();
        }
        public void AddClient(Client client)
        {
            Clients.Add(client);
        }
        public void RemoveClient(Client client)
        {
            Clients.Remove(client);
        }
        public void ClearClients()
        {
            Clients.Clear();
        }

    }
}
