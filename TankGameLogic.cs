using System;
using static Tanks.ConsoleInput;
using static Tanks.TankGemplayState;

namespace Tanks
{
	internal class TankGameLogic : BaseGameLogic, ConsoleInput.IArrowListener
    {
        private TankGemplayState gameplayState;
        private PlayerTankController playerController;

        public TankGameLogic()
        {
            var entityManager = new EntityManager();
            gameplayState = new TankGemplayState(entityManager);
            playerController = new PlayerTankController(entityManager);
        }

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
            gameplayState.SetDirection(TankGemplayState.TankDir.Up);
            playerController.MovePlayerTank();
        }

        public override void OnArrowDown()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(TankGemplayState.TankDir.Down);
            playerController.MovePlayerTank();
        }

        public override void OnArrowRight()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(TankGemplayState.TankDir.Right);
            playerController.MovePlayerTank();
        }

        public override void OnArrowLeft()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(TankGemplayState.TankDir.Left);
            playerController.MovePlayerTank();
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
            playerController.ShootPlayerTank();
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

