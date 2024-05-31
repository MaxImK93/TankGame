using System;
using static Tanks.TanksLogic.TankGemplayState;
using Tanks.Core;
using Tanks.Managers;
using Tanks.Rendering;
using Tanks.Interfaces;
using Tanks.TanksLogic;

namespace Tanks.Entities
{
	internal class Tank : IGameEntity
	{
        public enum TankType
        {
            Player,
            Enemy
        }

        public Cell Position { get; private set; } 
        public TankDir CurrentDir { get; set; }

        public bool IsAlive { get; private set; } = true; 
        public float Speed { get; private set; } = 1.0f;

        public TankType Type { get; private set; }
        private GameMap gameMap;

        private EntityManager entityManager;

        public Tank(Cell startPosition, TankDir startDir, TankType type, GameMap map, EntityManager entityManager)
		{
            Position = startPosition;
            CurrentDir = startDir;
            Type = type;
            gameMap = map;
            this.entityManager = entityManager;

        }

        public void Move()
        {
            var newPosition = TankGemplayState.ShiftTo(Position, CurrentDir);
            if (CanMoveTo(newPosition, entityManager.GetEntities()))
            {
                Position = newPosition;
            }
        }

        public bool CanMoveTo(Cell position, List<IGameEntity> entities)
        {
            char[,] tankShape = GetTankShape();

            for (int i = 0; i < tankShape.GetLength(0); i++)
            {
                for (int j = 0; j < tankShape.GetLength(1); j++)
                {
                    int x = position._X + j - 1;
                    int y = position._Y + i - 1;

                    if (!gameMap.IsWalkable(x, y, entities, this))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void Shoot()
        {
            var bulletPosition = TankGemplayState.ShiftTo(Position, CurrentDir);
            Console.WriteLine($"Танк на позиции ({Position._X}, {Position._Y}) стреляет. Пуля создана на позиции ({bulletPosition._X}, {bulletPosition._Y}) с направлением {CurrentDir}");
            var bullet = new Bullet(bulletPosition, CurrentDir, gameMap, entityManager.GetEntities(), entityManager);
            entityManager.AddEntity(bullet);
        }

        public void Draw(ConsoleRenderer renderer)
        {
            byte color = Type == TankType.Player ? (byte)2 : (byte)3;

            char[,] tankShape = GetTankShape();

            for (int i = 0; i < tankShape.GetLength(0); i++)
            {
                for (int j = 0; j < tankShape.GetLength(1); j++)
                {
                    int x = Position._X + j - 1; 
                    int y = Position._Y + i - 1; 
                    if (x >= 0 && x < renderer.width && y >= 0 && y < renderer.height) 
                    {
                        renderer.SetPixel(x, y, tankShape[i, j], color);
                    }
                }
            }
        }



        public char[,] GetTankShape()
        {
            switch (CurrentDir)
            {
                case TankDir.Up:
                    return TankShape.Up;
                case TankDir.Down:
                    return TankShape.Down;
                case TankDir.Left:
                    return TankShape.Left;
                case TankDir.Right:
                    return TankShape.Right;
                default:
                    return TankShape.Up;
            }
        }

        public void Update(float deltaTime)
        {
          
        }

        public void Destroy()
        {
            IsAlive = false; 
        }

    }
}

