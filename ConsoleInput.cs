using System;
using System.Collections.Generic;


namespace Tanks
{
    public class ConsoleInput
    {

        List<IArrowListener> arrowListeners = new List<IArrowListener>();

        public void Subscribe(IArrowListener listener)
        {
            arrowListeners.Add(listener);
        }

        public void Update()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKey keyInfo = Console.ReadKey(true).Key;

                switch (keyInfo)
                {
                    case ConsoleKey.UpArrow:
                        foreach (var listeners in arrowListeners)
                            listeners.OnArrowUp();
                        break;
                    case ConsoleKey.DownArrow:
                        foreach (var listeners in arrowListeners)
                            listeners.OnArrowDown();
                        break;
                    case ConsoleKey.RightArrow:
                        foreach (var listeners in arrowListeners)
                            listeners.OnArrowRight();
                        break;
                    case ConsoleKey.LeftArrow:
                        foreach (var listeners in arrowListeners)
                            listeners.OnArrowLeft();
                        break;

                }
            }


        }

        public interface IArrowListener
        {
            public abstract void OnArrowUp();

            public abstract void OnArrowDown();

            public abstract void OnArrowRight();

            public abstract void OnArrowLeft();


        }

    }
}

