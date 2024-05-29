using System;

namespace Tanks
{
    internal class GameMap
    {
        public enum ObstacleType
        {
            None,
            Wall,
            Tank,
            DamagedWall,
            Lake
        }

        public string[] firstLevelMap;
        private int width;
        private int height;

        public GameMap()
        {
            LoadMap();
        }

        private void LoadMap()
        {
             firstLevelMap = new string[]
            {
                "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                "▓▓▓▓                                                    ▓▓▓▓",
                "▓▓▓▓                                                    ▓▓▓▓",
                "▓▓▓▓                                                    ▓▓▓▓",
                "▓▓▓▓                                                    ▓▓▓▓",
                "▓▓▓▓                ▓▓▓▓                ▓▓▓▓            ▓▓▓▓",
                "▓▓▓▓                ▓▓▓▓                ▓▓▓▓            ▓▓▓▓",
                "▓▓▓▓                                                    ▓▓▓▓",
                "▓▓▓▓                                                    ▓▓▓▓",
                "▓▓▓▓              ~~~~~                                 ▓▓▓▓",
                "▓▓▓▓              ~~~~~                                 ▓▓▓▓",
                "▓▓▓▓              ~~~~~                                 ▓▓▓▓",
                "▓▓▓▓                                                    ▓▓▓▓",
                "▓▓▓▓                                                    ▓▓▓▓",
                "▓▓▓▓                                                    ▓▓▓▓",
                "▓▓▓▓                  ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                "▓▓▓▓                  ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                "▓▓▓▓                                                   ▓▓▓▓▓",
                "▓▓▓▓                                                   ▓▓▓▓▓",
                "▓▓▓▓                                                   ▓▓▓▓▓",
                "▓▓▓▓                           ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓    ▓▓▓▓▓",
                "▓▓▓▓                           ▓▓▓▓                    ▓▓▓▓▓",
                "▓▓▓▓                           ▓▓▓▓                    ▓▓▓▓▓",
                "▓▓▓▓                           ▓▓▓▓                    ▓▓▓▓▓",
                "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓"
            };

            height = firstLevelMap.Length;
            width = firstLevelMap[0].Length;
        }

        public void Draw(ConsoleRenderer renderer)
        {
            for (int y = 0; y < Math.Min(height, renderer.height); y++)
            {
                for (int x = 0; x < Math.Min(width, renderer.width); x++)
                {
                    renderer.SetPixel(x, y, firstLevelMap[y][x], 3);
                }
            }
        }

        public bool IsWalkable(int x, int y, List<IGameEntity> entities, Tank currentTank)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                return false;
            }

            if (firstLevelMap[y][x] == '▓')
            {
                return false;
            }

            foreach (var entity in entities)
            {
                if (entity is Tank tank && tank != currentTank)
                {
                    char[,] tankShape = tank.GetTankShape();
                    for (int i = 0; i < tankShape.GetLength(0); i++)
                    {
                        for (int j = 0; j < tankShape.GetLength(1); j++)
                        {
                            int tankX = tank.Position._X + j - 1;
                            int tankY = tank.Position._Y + i - 1;
                            if (x == tankX && y == tankY)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            if (firstLevelMap[y][x] == ' ')
            {
                return true;
            }

            return false;
        }

        public ObstacleType GetObstacleType(int x, int y, List<IGameEntity> entities, Tank currentTank)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                return ObstacleType.Wall; // За границами карты считаем стеной
            }

            if (firstLevelMap[y][x] == '▓')
            {
                return ObstacleType.Wall;
            }

            if (firstLevelMap[y][x] == '░')
            {
                return ObstacleType.DamagedWall;
            }

            if (firstLevelMap[y][x] == '~')
            {
                return ObstacleType.Lake;
            }

            foreach (var entity in entities)
            {
                if (entity is Tank tank && tank != currentTank)
                {
                    char[,] tankShape = tank.GetTankShape();
                    for (int i = 0; i < tankShape.GetLength(0); i++)
                    {
                        for (int j = 0; j < tankShape.GetLength(1); j++)
                        {
                            int tankX = tank.Position._X + j - 1;
                            int tankY = tank.Position._Y + i - 1;
                            if (x == tankX && y == tankY)
                            {
                                return ObstacleType.Tank;
                            }
                        }
                    }
                }
            }

            return ObstacleType.None;
        }

        public void DamageWall(int x, int y)
        {
            if (firstLevelMap[y][x] == '▓')
            {
                // Изменяем стену на поврежденную
                firstLevelMap[y] = firstLevelMap[y].Remove(x, 1).Insert(x, "░");
            }
            else if (firstLevelMap[y][x] == '░')
            {
                // Удаляем поврежденную стену
                firstLevelMap[y] = firstLevelMap[y].Remove(x, 1).Insert(x, " ");
            }
        }


        public int Width => width;
        public int Height => height;
    }
}
