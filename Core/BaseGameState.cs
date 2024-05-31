using System;
using Tanks.Rendering;

namespace Tanks.Core
{
    internal abstract class BaseGameState
    {
        public abstract void Update(float deltaTime);

        public abstract void Reset();

        public abstract void Draw(ConsoleRenderer renderer);

    }
}

