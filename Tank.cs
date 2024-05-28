using System;
using static Tanks.TankGemplayState;

namespace Tanks
{
	internal class Tank : IGameEntity
	{
        public enum TankType
        {
            Player,
            Enemy
        }

        public Cell Position { get; private set; } 
        public SnakeDir CurrentDir { get; set; } 
        public bool IsAlive { get; private set; } = true; 
        public float Speed { get; private set; } = 1.0f;
        public TankType Type { get; private set; }

        private GameMap gameMap;

        public Tank(Cell startPosition, SnakeDir startDir, TankType type, GameMap map)
		{
            Position = startPosition;
            CurrentDir = startDir;
            Type = type;
            gameMap = map;

        }

        private readonly char[,] tankShapeUp = new char[,]
        {
            { ' ', '╥',' '},
            { '╔', '═','╗'},
            { '╚', '═','╝'}
        };

        private readonly char[,] tankShapeDown = new char[,]
       {
            { '╔', '═', '╗' },
            { '╚', '═', '╝' },
            { ' ', '╨', ' ' }
       };

        private readonly char[,] tankShapeLeft = new char[,]
       {
            { ' ', '╔', '╗' },
            { '═', ' ', '║' },
            { ' ', '╚', '╝' }
       };

        private readonly char[,] tankShapeRight = new char[,]
       {
            { '╔', '╗', ' ' },
            { '║', ' ', '═' },
            { '╚', '╝', ' ' }
       };

        public void Move(List<IGameEntity> entities)
        {
            var newPosition = TankGemplayState.ShiftTo(Position, CurrentDir);
            if (CanMoveTo(newPosition, entities))
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
                case SnakeDir.Up:
                    return tankShapeUp;
                case SnakeDir.Down:
                    return tankShapeDown;
                case SnakeDir.Left:
                    return tankShapeLeft;
                case SnakeDir.Right:
                    return tankShapeRight;
                default:
                    return tankShapeUp;
            }
        }

        public void Update(float deltaTime)
        {
          
        }

    }
}

