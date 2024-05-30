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
            this.mapManager = mapManager;
            levels = new List<Level>();
            LoadLevels();
            currentLevelIndex = 0;
        }

        private void LoadLevels()
        {
            levels.Add(new Level(1, "Level1"));
            levels.Add(new Level(2, "Level2"));
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

