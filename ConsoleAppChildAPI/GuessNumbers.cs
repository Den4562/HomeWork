using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppChildAPI
{
    public class GuessNumbers
    {
        public void PlayGame()
        {
            Random random = new Random();
            int min = 0;
            int max = 100;
            int num = random.Next(min, max + 1);
            int attempts = 0;
            int userGuess;

            Console.WriteLine($"Попробуйте угадать число от {min} до {max}.");

            do
            {
                Console.Write("Введите число: ");
                if (int.TryParse(Console.ReadLine(), out userGuess))
                {
                    attempts++;
                    if (userGuess < num)
                    {
                        Console.WriteLine("Загаданное число больше.");
                    }
                    else if (userGuess > num)
                    {
                        Console.WriteLine("Загаданное число меньше.");
                    }
                    else
                    {
                        Console.WriteLine($"Ви угадали число {num} за {attempts} попыток.");
                    }
                }
                else
                {
                    Console.WriteLine("Пожалуйста введите правельное число.");
                }
            } while (userGuess != num);

        }
    }
}
