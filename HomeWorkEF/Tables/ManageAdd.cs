using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkEF.Tables
{
    public class ManageAdd
    {
        public static void AddGroup(AcademiaDBContext db)
        {
            Console.WriteLine("Название Группы:");
            string name = Console.ReadLine();

            Console.WriteLine("Рейтинг группы (1-5):");
            int rating = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Год обучения (1-5):");
            int year = Convert.ToInt32(Console.ReadLine());

            var newGroup = new Group
            {
                Name = name,
                Rating = rating,
                Year = year
            };

            db.Group.Add(newGroup);

            db.SaveChanges();

            Console.WriteLine("Запись для Таблицы 'Group' добавлены.");
            Console.WriteLine();
        }

        public static void AddDepartment(AcademiaDBContext db)
        {
            Console.WriteLine("Финансирование:");
            int financing = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Название:");
            string name = Console.ReadLine();



            var newDepartment = new Department
            {
                Financing = financing,
                Name = name
            };

            db.Department.Add(newDepartment);

            db.SaveChanges();

            Console.WriteLine("Запись для Таблицы 'Department' добавлены.");
            Console.WriteLine();
        }


        public static void AddFaculties(AcademiaDBContext db)
        {
         
            Console.WriteLine("Название:");
            string ? name = Console.ReadLine();
            var newFaculties = new Faculties
            {
                Name = name
            };

            db.Faculties.Add(newFaculties);

            db.SaveChanges();

            Console.WriteLine("Запись для Таблицы 'Department' добавлены.");
            Console.WriteLine();
        }



        public static void AddTeacher(AcademiaDBContext db)
        {
            Console.WriteLine("Имя преподавателя:");
            string name = Console.ReadLine();

            Console.WriteLine("Фамилия преподавателя:");
            string surname = Console.ReadLine();

            DateTime date;
            do
            {
                Console.WriteLine("Дата трудоустройства (yyyy-MM-dd):");
            } while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date) || date < new DateTime(1990, 1, 1));

            Console.WriteLine("Премия:");
            decimal premium;
            premium = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Зарплата:");
            decimal salary;
            salary = Convert.ToDecimal(Console.ReadLine());

            var newTeacher = new Teacher
            {
                Name = name,
                Surname = surname,
                EmploymentDate = date,
                Premium = premium,
                Salary = salary
            };

            db.Teacher.Add(newTeacher);

            db.SaveChanges();

            Console.WriteLine("Запись для Таблицы 'Teacher' добавлена.");
            Console.WriteLine();
        }

        public static void Menu()
        {
            Console.WriteLine("Выберите таблицу для отображения:");
            Console.WriteLine("1. Группы");
            Console.WriteLine("2. Факультеты");
            Console.WriteLine("3. Преподаватели");
            Console.WriteLine("4. Корпус");
            Console.WriteLine("5. Добавить записи в таблицы");
            Console.WriteLine("6. Выход");
        }

    }
}
