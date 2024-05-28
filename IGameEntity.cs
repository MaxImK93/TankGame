using System;
namespace Tanks
{
    internal interface IGameEntity
    {
        void Update(float deltaTime);
        void Draw(ConsoleRenderer renderer);
        bool IsAlive { get; }
        
    }

}

