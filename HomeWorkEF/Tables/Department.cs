using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkEF.Tables
{
    public class Department
    {
        public int Id { get; set; }
        public decimal Financing { get; set; }
        public string Name { get; set; }

        public Department()
        {
        }

        public Department(int id, decimal financing, string name)
        {
            Id = id;
            Financing = financing;
            Name = name;
        }

        public override string ToString()
        {
            return $"Id:{Id} | Financing:{Financing} | Name: {Name}";
        }
    }
}
