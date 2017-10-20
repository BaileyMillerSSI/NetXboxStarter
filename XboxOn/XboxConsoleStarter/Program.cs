using System;
using XboxStarter;

namespace XboxConsoleStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Xbox One Starter";
            Console.WriteLine("Attempting to launch Xbox One ...");

            XboxOn.ImportSettings("", "");

            XboxOn.PowerOnXboxOne();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
