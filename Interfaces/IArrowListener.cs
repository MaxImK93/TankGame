using System;

namespace Tanks.Interfaces
{
    public interface IArrowListener
    {
        public abstract void OnArrowUp();

        public abstract void OnArrowDown();

        public abstract void OnArrowRight();

        public abstract void OnArrowLeft();

        public abstract void OnShoot();

    }
}

