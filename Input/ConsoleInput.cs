using System;
using System.Collections.Generic;
using Tanks.Interfaces;

namespace Tanks.Input
{
    public class ConsoleInput : IArrowListener
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
                    case ConsoleKey.Spacebar:
                        foreach (var listeners in arrowListeners)
                            listeners.OnShoot();
                        break;

                }
            }

        }

        public void OnArrowDown()
        {
            throw new NotImplementedException();
        }

        public void OnArrowLeft()
        {
            throw new NotImplementedException();
        }

        public void OnArrowRight()
        {
            throw new NotImplementedException();
        }

        public void OnArrowUp()
        {
            throw new NotImplementedException();
        }

        public void OnShoot()
        {
            throw new NotImplementedException();
        }

    }
}

