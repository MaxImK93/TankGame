using System;
using static Tanks.GameMap;
using static Tanks.TankGemplayState;

namespace Tanks
{
	internal class Bullet : IGameEntity
	{
        public Cell Position { get; private set; }
        public SnakeDir Direction { get; private set; }

        public bool IsAlive => throw new NotImplementedException();

        private GameMap gameMap;
        private List<IGameEntity> entities;

        private TankGemplayState gameState;

        public Bullet(Cell startPosition, SnakeDir direction, GameMap map, TankGemplayState gameState, List<IGameEntity> entities)
        {
            Position = startPosition;
            Direction = direction;
            gameMap = map;
            this.gameState = gameState;
            this.entities = entities;
        }

        public void Update(float deltaTime)
        {
            Move();
        }

        public void Draw(ConsoleRenderer renderer)
        {
            if (Position._X >= 0 && Position._X < renderer.width && Position._Y >= 0 && Position._Y < renderer.height)
            {
                renderer.SetPixel(Position._X, Position._Y, '•', 1);
            }
        }

        public void Move()
        {
            Cell newPosition = TankGemplayState.ShiftTo(Position, Direction);
            Console.WriteLine($"Попытка переместить пулю в ({newPosition._X}, {newPosition._Y})");

            var obstacle = gameMap.GetObstacleType(newPosition._X, newPosition._Y, entities, null);

            if (obstacle == ObstacleType.Wall)
            {
                Console.WriteLine($"Пуля столкнулась со стеной в ({newPosition._X}, {newPosition._Y})");
                gameState.RemoveEntity(this);
                return;
            }
            else if (obstacle == ObstacleType.Tank)
            {
                Console.WriteLine($"Пуля попала в танк на позиции ({newPosition._X}, {newPosition._Y})");
                gameState.RemoveEntity(this);
                foreach (var entity in entities)
                {
                    if (entity is Tank tank)
                    {
                        char[,] tankShape = tank.GetTankShape();
                        for (int i = 0; i < tankShape.GetLength(0); i++)
                        {
                            for (int j = 0; j < tankShape.GetLength(1); j++)
                            {
                                int tankX = tank.Position._X + j - 1;
                                int tankY = tank.Position._Y + i - 1;
                                if (newPosition._X == tankX && newPosition._Y == tankY)
                                {
                                    gameState.RemoveEntity(tank);
                                    break;
                                }
                            }
                        }
                    }
                }
                return;
            }

            Position = newPosition;
        }


    }
}

