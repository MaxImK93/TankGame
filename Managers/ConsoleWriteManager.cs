using System;
using Tanks.Managers;
using Tanks.TanksLogic;

namespace Tanks.Managers
{
	internal class ConsoleWriteManager
	{
        
		public void LevelOverWrite(string text)
		{
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
            Console.WriteLine(text);
            Thread.Sleep(3000);
        }

        public void LevelStartMessage(int level)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2);
            Console.WriteLine($"Level {level} Completed!");
            Thread.Sleep(3000);
        }

        public void StartingLevelMessage(int level)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2);
            Console.WriteLine($"Starting Level {level}");
            Thread.Sleep(3000);
            Console.Clear();
        }

	}
}

