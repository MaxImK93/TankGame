using System;
using Tanks.Rendering;

namespace Tanks.Interfaces
{
    internal interface IGameEntity
    {
        void Update(float deltaTime);
        void Draw(ConsoleRenderer renderer);
        bool IsAlive { get; }
        
    }

}

