using System;
using Tanks.Core;
using Tanks.Managers;
using Tanks.Entities;
using Tanks.Rendering;
using Tanks.Controllers;

namespace Tanks.TanksLogic
{
	internal class TankGemplayState : BaseGameState
	{
        public enum TankDir
        {
            Up,
            Down,
            Right,
            Left
        }

        public TankDir currentDir = TankDir.Left;

        private LevelManager levelManager;
        private GameMap gameMap;
        private MapManager mapManager;

        private float timeToMove = 0f;
        private const float MoveInterval = 1f / 4f;

        private List<EnemyTankLogic> enemyLogics;

        private EntityManager entityManager;
        private PlayerTankController playerTankController;

        Cell tank;

        public int fieldWidth { get; set; }
        public int fieldHeight { get; set; }

        private Level currentLevel;

        public TankGemplayState(EntityManager entityManager, LevelManager levelManager,MapManager mapManager)
        {
            this.entityManager = entityManager;
            this.levelManager = levelManager;
            this.mapManager = mapManager;
            enemyLogics = new List<EnemyTankLogic>();
            playerTankController = new PlayerTankController(entityManager);
        }

        public void SetDirection(TankDir Direction)
        {
            Tank playerTank = playerTankController.GetPlayerTank();
            if (playerTank != null)
            {
                playerTank.CurrentDir = Direction;
            }
        }

        public override void Reset()
        {

            LoadLevel();
        }

        private void LoadLevel()
        {
            currentLevel = levelManager.GetCurrentLevel();
            gameMap = new GameMap(mapManager.GetMap(currentLevel.MapKey));

            entityManager.Clear();

            var playerTank = new Tank(currentLevel.PlayerStartPosition, TankDir.Left, Tank.TankType.Player, gameMap, entityManager);
            entityManager.AddEntity(playerTank);

            foreach (var enemyPosition in currentLevel.EnemyStartPositions)
            {
                var enemyTank = new Tank(enemyPosition, TankDir.Right, Tank.TankType.Enemy, gameMap, entityManager);
                entityManager.AddEntity(enemyTank);
                enemyLogics.Add(new EnemyTankLogic(enemyTank, entityManager.GetEntities(), entityManager, mapManager));
            }

            currentDir = TankDir.Left;
            timeToMove = 0f;
        }
        

        public override void Draw(ConsoleRenderer renderer)
        {
            gameMap.Draw(renderer);

            foreach (var entity in entityManager.GetEntities())
            {
                entity.Draw(renderer);
            }
        }

        public override void Update(float deltaTime)
        {
            timeToMove -= deltaTime;

            if (timeToMove > 0f)
                return;

            timeToMove = MoveInterval;

            entityManager.UpdateEntities(deltaTime);
            UpdateEnemyLogic(deltaTime);

            entityManager.ProcessEntityChanges();

            CheckLevelCompletion();
        }

        private void UpdateEnemyLogic(float deltaTime)
        {

            List<EnemyTankLogic> logicsToRemove = new List<EnemyTankLogic>();


            foreach (var logic in enemyLogics)
            {
                if (!logic.IsTankAlive())
                {
                    logicsToRemove.Add(logic);
                }
            }


            foreach (var logic in logicsToRemove)
            {
                enemyLogics.Remove(logic);
            }

            foreach (var logic in enemyLogics)
            {
                logic.Update(deltaTime);
            }
        }

        private void CheckLevelCompletion()
        {
            var currentLevel = levelManager.GetCurrentLevel();
            if (currentLevel != null)
            {
                bool isLevelCompleted = currentLevel.IsLevelCompleted(entityManager);
                bool isGameOver = currentLevel.IsGameOver(entityManager);

                if (isGameOver)
                {
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
                    Console.WriteLine("Game Over");
                    Thread.Sleep(3000);
                    levelManager.ResetLevels();
                    Reset();
                }
                else if (isLevelCompleted)
                {
                    if (levelManager.IsLastLevel())
                    {
                        Console.Clear();
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2);
                        Console.WriteLine("Congratulations! You completed the game!");
                        Thread.Sleep(3000);
                        levelManager.ResetLevels();
                        Reset();
                    }
                    else
                    {
                        Console.Clear();
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2);
                        Console.WriteLine($"Level {currentLevel.LevelNumber} Completed!");
                        Thread.Sleep(3000);
                        levelManager.NextLevel();
                        Reset();
                    }
                }
            }
        }


        private void ShowLevelStartMessage(int levelNumber)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2);
            Console.WriteLine($"Starting Level {levelNumber}");
            Thread.Sleep(3000);
            Console.Clear();
        }

        public static Cell ShiftTo(Cell from, TankDir dir)
        {

            switch (dir)
            {
                case TankDir.Up:
                    return new Cell(from._X, from._Y - 1);
                case TankDir.Down:
                    return new Cell(from._X, from._Y + 1);
                case TankDir.Right:
                    return new Cell(from._X + 1, from._Y);
                case TankDir.Left:
                    return new Cell(from._X - 1, from._Y);

            }

            return from;
        }

        public struct Cell
        {
            public int _X;
            public int _Y;

            public Cell(int x, int y)
            {
                _X = x;
                _Y = y;
            }

        }
    }
}

