using System;
using static Tanks.ConsoleInput;
using static Tanks.TankGemplayState;

namespace Tanks
{
	internal class TankGameLogic : BaseGameLogic, ConsoleInput.IArrowListener
    {
        TankGemplayState gameplayState = new TankGemplayState();


        public void GotoGameplay()
        {

            gameplayState.fieldHeight = screenHeight;
            gameplayState.fieldWidth = screenWidth;

            ChangeState(gameplayState);
            gameplayState.Reset();
        }

        public override void OnArrowUp()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(TankGemplayState.SnakeDir.Up);
            gameplayState.MovePlayerTank();
        }

        public override void OnArrowDown()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(TankGemplayState.SnakeDir.Down);
            gameplayState.MovePlayerTank();
        }

        public override void OnArrowRight()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(TankGemplayState.SnakeDir.Right);
            gameplayState.MovePlayerTank();
        }

        public override void OnArrowLeft()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(TankGemplayState.SnakeDir.Left);
            gameplayState.MovePlayerTank();
        }

        public override void Update(float deltaTime)
        {
            if (currentState != gameplayState)
            {
                GotoGameplay();
            }
        }

        public override void OnShoot()
        {
            gameplayState.ShootPlayerTank();
        }

        public override ConsoleColor[] CreatePalette()
        {
            return new ConsoleColor[]
            {
                ConsoleColor.Black,
                ConsoleColor.Gray,
                ConsoleColor.Red,
                ConsoleColor.Blue

            };
        }
    }
}

