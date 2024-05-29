using System;
namespace Tanks
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

        private GameMap gameMap;

        private float timeToMove = 0f;
        private const float MoveInterval = 1f / 4f;

        private List<EnemyTankLogic> enemyLogics;

        private EntityManager entityManager;
        private PlayerTankController playerTankController;

        Cell tank;

        public int fieldWidth { get; set; }
        public int fieldHeight { get; set; }

        public TankGemplayState(EntityManager entityManager)
        {
            gameMap = new GameMap();
            enemyLogics = new List<EnemyTankLogic>();
            this.entityManager = entityManager;
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

            var playerTank = new Tank(new Cell(15, 20), TankDir.Left, Tank.TankType.Player, gameMap, entityManager);
            var enemyTank1 = new Tank(new Cell(10, 15), TankDir.Right, Tank.TankType.Enemy, gameMap, entityManager);
            var enemyTank2 = new Tank(new Cell(10, 20), TankDir.Left, Tank.TankType.Enemy, gameMap, entityManager);

            entityManager.AddEntity(playerTank);
            entityManager.AddEntity(enemyTank1);
            entityManager.AddEntity(enemyTank2);

            enemyLogics.Add(new EnemyTankLogic(enemyTank1, entityManager.GetEntities(), this, entityManager));
            enemyLogics.Add(new EnemyTankLogic(enemyTank2, entityManager.GetEntities(), this, entityManager));

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
        }

        private void UpdateEnemyLogic(float deltaTime)
        {
            foreach (var logic in enemyLogics)
            {
                logic.Update(deltaTime);
            }
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

