using Microsoft.Data.SqlClient;
using HomeWorkEF.Tables;
using Microsoft.EntityFrameworkCore;

namespace HomeWorkEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice=0;
            int add = 0;

            try
			{
               
                using (AcademiaDBContext db = new AcademiaDBContext())
				{
                    do
                    {
            
                        ManageAdd.Menu();
                        choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
					    {
						    case 1:
							    {
                                    Console.Clear();
                                    var groups = db.Group.ToList();
                                    foreach (var group in groups)
                                    {
                                        System.Console.WriteLine(group);
                                    }
                                    Console.WriteLine();
                                   
                                    break; 
                                  
							    }

                            case 2:
                                {
                                    Console.Clear();
                                    var faculties = db.Faculties.ToList();
                                    foreach (var faculty in faculties)
                                    {
                                        System.Console.WriteLine(faculty);
                                    }
                                    Console.WriteLine();
                                    break;
                                }

                            case 3:
                                {
                                    Console.Clear();
                                    var teachers = db.Teacher.ToList();
                                    foreach (var teacher in teachers)
                                    {
                                        System.Console.WriteLine(teacher);
                                    }

                                    Console.WriteLine();
                                    break;
                                }

                            case 4:
                                {
                                    Console.Clear();
                                    var departmens = db.Department.ToList();
                                    foreach (var department in departmens)
                                    {
                                        System.Console.WriteLine(department);
                                    }

                                    Console.WriteLine();
                                    break;
                                }
                           
                            case 5:
                                {
                                    Console.Clear();
                                    Console.WriteLine("Выберите таблицу для Добавление записи:");
                                    Console.WriteLine("1. Группы");
                                    Console.WriteLine("2. Факультеты");
                                    Console.WriteLine("3. Преподаватели");
                                    Console.WriteLine("4. Корпус");
                                    Console.WriteLine("5. Выход");
                                    add = Convert.ToInt32(Console.ReadLine());
                                    switch (add)
                                    {
                                        case 1:
                                            {
                                                Console.Clear();
                                                ManageAdd.AddGroup(db);
                                                break;
                                            }
                                        case 2:
                                            {
                                                Console.Clear();
                                                ManageAdd.AddFaculties(db);
                                                break;
                                            }
                                        case 3:
                                            {
                                                Console.Clear();
                                                ManageAdd.AddTeacher(db);
                                                break;
                                            }
                                        case 4:
                                            {
                                                Console.Clear();
                                                ManageAdd.AddDepartment(db);
                                                break;
                                            }

                                        default:
                                            break;
                                    }
                                   
                                    break;
                                }

                            
                            default:
							    break;
					    }
                    } while (choice!=6);
                }
			}
			catch (DbUpdateException ex)
			{
                Console.WriteLine("Ошибка при сохранении данных в базе данных:");
                System.Console.WriteLine(ex.Message);

			}
			catch (ApplicationException ex)
			{
				System.Console.WriteLine(ex.Message);
			}
			catch (System.Exception ex)
			{
                System.Console.WriteLine(ex.Message);
            }

        }
    }
}