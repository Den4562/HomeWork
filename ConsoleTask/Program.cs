using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static CancellationTokenSource stopTask;

    static void Main()
    {
       
        Console.WriteLine("1. метод Start класа Task");
        Console.WriteLine("2. метод Task.Factory.StartNew");
        Console.WriteLine("3. метод Task.Run");
        Console.Write("Введите номер способа: ");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            stopTask = new CancellationTokenSource();

            switch (choice)
            {
                case 1:
                    startTask();
                    break;
                case 2:
                    factoryTask();
                    break;
                case 3:
                    TaskRun();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Завершение программы.");
                    break;
            }

            // Дозволити користувачу зупинити завдання за допомогою клавіші Enter
            Console.WriteLine("Нажмите 'Enter' для завершение программы.");
            Console.ReadLine();
            stopTask.Cancel();
        }
        else
        {
            Console.WriteLine("Неверный выбор. Завершение программы.");
        }
    }

    static void dataTime()
    {
        while (!stopTask.Token.IsCancellationRequested)
        {
            Console.WriteLine($"Время и дата: {DateTime.Now}");
            Thread.Sleep(1000); // Затримка 1 секунда
        }
    }

    static void startTask()
    {
        Task task = new Task(dataTime, stopTask.Token);
        task.Start();
    }

    static void factoryTask()
    {
        Task.Factory.StartNew(dataTime, stopTask.Token);
    }

    static void TaskRun()
    {
        Task.Run(dataTime, stopTask.Token);
    }
}