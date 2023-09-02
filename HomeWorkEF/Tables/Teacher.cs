using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkEF.Tables
{
    public class Teacher
    {
        public int Id { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string Name { get; set; }
        public decimal Premium { get; set; }
        public decimal Salary { get; set; }
        public string Surname { get; set; }

        public Teacher()
        {
            EmploymentDate = DateTime.Now;
            Premium = 0;
            Salary = 0;
        }

        public Teacher(int id, DateTime employmentDate, string name, decimal premium, decimal salary, string surname)
        {
            Id = id;
            EmploymentDate = employmentDate;
            Name = name;
            Premium = premium;
            Salary = salary;
            Surname = surname;
        }

        public override string ToString()
        {
            return $"Id: {Id} | Employment Date: {EmploymentDate} | Name: {Name} | Premium: {Premium} | Salary: {Salary} | Surname: {Surname}";
        }
    }
}
