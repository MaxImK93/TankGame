using System;
namespace Tanks
{
	internal class TankGemplayState : BaseGameState
	{
        public enum SnakeDir
        {
            Up,
            Down,
            Right,
            Left
        }

        public SnakeDir currentDir = SnakeDir.Left;

        private List<IGameEntity> entities;
        private GameMap gameMap; 
        private float timeToMove = 0f;
        private const float MoveInterval = 1f / 4f;
        private List<EnemyTankLogic> enemyLogics;


        public TankGemplayState()
        {
            entities = new List<IGameEntity>();
            gameMap = new GameMap();
            enemyLogics = new List<EnemyTankLogic>();
        }

        public int fieldWidth { get; set; }
        public int fieldHeight { get; set; }

        Cell tank;

        public void SetDirection(SnakeDir Direction)
        {
            Tank playerTank = GetPlayerTank();
            if (playerTank != null)
            {
                playerTank.CurrentDir = Direction;
            }
        }

        public override void Reset()
        {
            var middleY = fieldHeight / 2;
            var middleX = fieldWidth / 2;

            var playerTank = new Tank(new Cell(15, 15), SnakeDir.Right, Tank.TankType.Player, gameMap);
            var enemyTank1 = new Tank(new Cell(17, 10), SnakeDir.Right, Tank.TankType.Enemy, gameMap);
            var enemyTank2 = new Tank(new Cell(20, 13), SnakeDir.Left, Tank.TankType.Enemy, gameMap);

            entities.Add(playerTank);
            entities.Add(enemyTank1);
            entities.Add(enemyTank2);

            enemyLogics.Add(new EnemyTankLogic(enemyTank1, entities));
            enemyLogics.Add(new EnemyTankLogic(enemyTank2, entities));

            currentDir = SnakeDir.Left;

            timeToMove = 0f;
        }

        public override void Draw(ConsoleRenderer renderer)
        {
            gameMap.Draw(renderer);

            foreach (var entity in entities)
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

            foreach (var entity in entities)
            {
                entity.Update(deltaTime);
            }

            foreach (var logic in enemyLogics)
            {
                logic.Update(deltaTime);
            }

        }

        public void MovePlayerTank()
        {
            Tank playerTank = GetPlayerTank();
            if (playerTank != null)
            {
                playerTank.Move(entities);
            }
        }

        public static Cell ShiftTo(Cell from, SnakeDir dir)
        {

            switch (dir)
            {
                case SnakeDir.Up:
                    return new Cell(from._X, from._Y - 1);
                case SnakeDir.Down:
                    return new Cell(from._X, from._Y + 1);
                case SnakeDir.Right:
                    return new Cell(from._X + 1, from._Y);
                case SnakeDir.Left:
                    return new Cell(from._X - 1, from._Y);

            }

            return from;
        }

        private Tank GetPlayerTank()
        {
            foreach (var entity in entities)
            {
                if (entity is Tank tank && tank.Type == Tank.TankType.Player)
                {
                    return tank;
                }
            }
            return null;
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

