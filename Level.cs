using System;
namespace Tanks
{
	internal class Level
	{
        public string MapKey { get; }
        public int LevelNumber { get; }
        public TankGemplayState.Cell PlayerStartPosition { get; }
        public List<TankGemplayState.Cell> EnemyStartPositions { get; }

        public Level(int levelNumber, string mapKey, TankGemplayState.Cell playerStartPosition, List<TankGemplayState.Cell> enemyStartPositions)
        {
            LevelNumber = levelNumber;
            MapKey = mapKey;
            PlayerStartPosition = playerStartPosition;
            EnemyStartPositions = enemyStartPositions;
        }

        public bool IsLevelCompleted(EntityManager entityManager)
        {
            foreach (var entity in entityManager.GetEntities())
            {
                if (entity is Tank tank && tank.Type == Tank.TankType.Enemy)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsGameOver(EntityManager entityManager)
        {
            foreach (var entity in entityManager.GetEntities())
            {
                if (entity is Tank tank && tank.Type == Tank.TankType.Player)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

