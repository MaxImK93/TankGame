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

        private List<IGameEntity> entitiesToAdd;
        private List<IGameEntity> entitiesToRemove;


        public TankGemplayState()
        {
            entities = new List<IGameEntity>();
            gameMap = new GameMap();
            enemyLogics = new List<EnemyTankLogic>();

            entitiesToAdd = new List<IGameEntity>();
            entitiesToRemove = new List<IGameEntity>();
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

            var playerTank = new Tank(new Cell(15, 15), SnakeDir.Left, Tank.TankType.Player, gameMap);
            var enemyTank1 = new Tank(new Cell(10, 15), SnakeDir.Right, Tank.TankType.Enemy, gameMap);
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

            if (entitiesToAdd.Count > 0 || entitiesToRemove.Count > 0)
            {
                Console.WriteLine($"Добавлено сущностей: {entitiesToAdd.Count}");
                foreach (var entity in entitiesToAdd)
                {
                    entities.Add(entity);
                }
                entitiesToAdd.Clear();

                Console.WriteLine($"Удалено сущностей: {entitiesToRemove.Count}");
                foreach (var entity in entitiesToRemove)
                {
                    Console.WriteLine($"Удаляем сущность: {entity.GetType().Name}");
                    entities.Remove(entity);
                }
                entitiesToRemove.Clear();
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

        public void ShootPlayerTank()
        {
            Console.WriteLine("ShootPlayerTank вызван");
            Tank playerTank = GetPlayerTank();
            if (playerTank != null)
            {
                Console.WriteLine("Игрок стреляет");
                playerTank.Shoot(entitiesToAdd, this);
            }
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

        public void AddEntity(IGameEntity entity)
        {
            entitiesToAdd.Add(entity);
        }

        public void RemoveEntity(IGameEntity entity)
        {
            entitiesToRemove.Add(entity);
        }

        public List<IGameEntity> GetEntities()
        {
            return entities;
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

