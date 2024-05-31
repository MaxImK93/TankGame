using System;
using static Tanks.Input.ConsoleInput;
using static Tanks.TanksLogic.TankGemplayState;
using Tanks.Core;
using Tanks.Managers;
using Tanks.Input;
using Tanks.Controllers;
using Tanks.TanksLogic;

namespace Tanks.TanksLogic
{
	internal class TankGameLogic : BaseGameLogic, ConsoleInput.IArrowListener
    {
        private TankGemplayState gameplayState;
        private PlayerTankController playerController;
        private LevelManager levelManager;
        private MapManager mapManager;

        public TankGameLogic(EntityManager entityManager, LevelManager levelManager, MapManager mapManager)
        {
            this.levelManager = levelManager;
            this.mapManager = mapManager;
            gameplayState = new TankGemplayState(entityManager, levelManager, mapManager);
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

