using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int[] array = { 10, 5, 8, 15, 3, 7 };

        Task<int> minTask = Task.Run(() => Min(array));
        Task<int> maxTask = Task.Run(() => Max(array));
        Task<double> averageTask = Task.Run(() => Average(array));
        Task<int> sumTask = Task.Run(() => Sum(array));

        Task.WhenAll(minTask, maxTask, averageTask, sumTask).Wait();

        int min = minTask.Result;
        int max = maxTask.Result;
        double average = averageTask.Result;
        int sum = sumTask.Result;
        Console.WriteLine("Масив: " + string.Join(", ", array)); // Вивести масив
        Console.WriteLine("Минимум: " + min);
        Console.WriteLine("Максимум: " + max);
        Console.WriteLine("Среднее арифметическое: " + average);
        Console.WriteLine("Сумма: " + sum);

        Console.ReadLine();
    }

    static int Min(int[] array)
    {
        return array.Min();
    }

    static int Max(int[] array)
    {
        return array.Max();
    }

    static double Average(int[] array)
    {
        return array.Average();
    }

    static int Sum(int[] array)
    {
        return array.Sum();
    }
}