using System;
namespace Tanks
{
	internal class LevelManager
	{
        private List<Level> levels;
        private int currentLevelIndex;
        private MapManager mapManager;

        public LevelManager(MapManager mapManager)
        {
                levels = new List<Level>
            {

                 new Level(1, "Level1", new TankGemplayState.Cell(47, 23), new List<TankGemplayState.Cell>
                {
                    new TankGemplayState.Cell(4, 3)

                }),
                new Level(2, "Level2", new TankGemplayState.Cell(36, 23), new List<TankGemplayState.Cell>
                {
                    new TankGemplayState.Cell(4, 3),
                    new TankGemplayState.Cell(52, 9)

                }),
                new Level(3, "Level3", new TankGemplayState.Cell(47, 23), new List<TankGemplayState.Cell>
                {
                    new TankGemplayState.Cell(4, 3),
                    new TankGemplayState.Cell(52, 9),
                    new TankGemplayState.Cell(25, 3)
                })
            };

                currentLevelIndex = 0;
            
        }

        public Level GetCurrentLevel()
        {
            if (currentLevelIndex >= 0 && currentLevelIndex < levels.Count)
            {
                return levels[currentLevelIndex];
            }
            return null;
        }

        public void NextLevel()
        {
            currentLevelIndex++;
            if (currentLevelIndex >= levels.Count)
            {
                Console.WriteLine("Congratulations! You have completed all levels!");
                ResetLevels();
            }
        }

        public bool IsLastLevel()
        {
            return currentLevelIndex == levels.Count - 1;
        }

        public void ResetLevels()
        {
            currentLevelIndex = 0;
        }
    }
}

