using System;
namespace Tanks
{
	internal abstract class BaseGameLogic : ConsoleInput.IArrowListener
	{
        protected BaseGameState? currentState { get; private set; }
        protected float time { get; private set; }
        protected int screenWidth { get; private set; }
        protected int screenHeight { get; private set; }

        public abstract ConsoleColor[] CreatePalette();

        public virtual void OnArrowDown()
        {
            throw new NotImplementedException();
        }

        public virtual void OnArrowLeft()
        {
            throw new NotImplementedException();
        }

        public virtual void OnArrowRight()
        {
            throw new NotImplementedException();
        }

        public virtual void OnArrowUp()
        {
            throw new NotImplementedException();
        }

        public virtual void OnShoot()
        {
            throw new NotImplementedException();
        }

        public void InitializeInput(ConsoleInput input)
        {
            input.Subscribe(this);
        }

        public void ChangeState(BaseGameState state)
        {
            currentState?.Reset();

            currentState = state;

        }

        public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
        {
            time += deltaTime;
            screenWidth = renderer.width;
            screenHeight = renderer.height;

            currentState?.Update(deltaTime);
            currentState?.Draw(renderer);

            Update(deltaTime);
        }

        public abstract void Update(float deltaTime);

       
    }
}

