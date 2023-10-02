using ConsoleAppChildAPI;
using System;
using System.Runtime.InteropServices;

class Program
{
    
    
    
    
    // Імпортуємо функцію MessageBox з Windows API
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    static void Main()
    {
      
        MessageBox(IntPtr.Zero, "Hello, World!", "MessageBox Example", 0);



        Console.WriteLine("Игра: 'Угадайте число'");
        do
        {
            GuessNumbers game = new GuessNumbers();
            game.PlayGame();
            Console.WriteLine("Хотите сыграть еще раз ? (Да или нет)");
        } while (Console.ReadLine()?.ToLower() == "да");
    }
}