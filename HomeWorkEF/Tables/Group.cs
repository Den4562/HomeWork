using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkEF.Tables
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public int Year { get; set; }

        public Group() 
        {
        }

        public Group(int id, string name, int rating, int year)
        {
            Id = id;
            Name = name;
            Rating = rating;
            Year = year;
        }

        public override string ToString()
        {
            return $"Id:{Id} | Name: {Name} | Rating: {Rating} Year: {Year}";
        }
    }
}