using System;
namespace Tanks
{
	internal class Level
	{
        public string MapKey { get; }
        public int LevelNumber { get; }

        public Level(int levelNumber, string mapKey)
        {
            LevelNumber = levelNumber;
            MapKey = mapKey;
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

