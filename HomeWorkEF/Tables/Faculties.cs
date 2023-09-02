using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkEF.Tables
{
    public class Faculties
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Faculties()
        { }

        public Faculties(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"Id:{Id} | Name: {Name}";
        }

    }
}