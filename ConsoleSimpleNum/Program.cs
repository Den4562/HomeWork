using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {


        // Створюємо завдання для пошуку простих чисел
        Task<List<int>> task = Task.Run(() => FindPrimes(0, 1000));

        // Очікуємо завершення завдання
        task.Wait();

        // Отримуємо результат і виводимо прості числа
        List<int> primes = task.Result;
        Console.WriteLine("Простые числа в диапазоне от 0 до 1000:");
        foreach (int prime in primes)
        {
            Console.Write(prime + " ");
        }

        // Виводимо кількість знайдених простих чисел
        Console.WriteLine($"\nКоличество простых чисел: {primes.Count}");

        Console.ReadLine();
    }

    static List<int> FindPrimes(int start, int end)
    {
        List<int> primes = new List<int>();

        for (int number = start; number <= end; number++)
        {
            if (IsPrime(number))
            {
                primes.Add(number);
            }
        }

        return primes;
    }

    static bool IsPrime(int number)
    {
        if (number <= 1)
        {
            return false;
        }

        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }
}