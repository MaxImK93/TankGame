using System;
namespace Tanks
{
	internal class MapManager
	{
        private Dictionary<string, string[]> maps;
        private string currentMapKey;

        public MapManager()
        {
            maps = new Dictionary<string, string[]>();
            LoadMaps();
        }

        private void LoadMaps()
        {
            maps = new Dictionary<string, string[]>
            {
                ["Level1"] = new string[]
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
                },

                ["Level2"] = new string[]
                {
                    "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                    "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                                                    ▓▓▓▓",
                    "▓▓▓▓                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                    "▓▓▓▓                       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                    "▓▓▓▓                                                   ▓▓▓▓▓",
                    "▓▓▓▓                                                   ▓▓▓▓▓",
                    "▓▓▓▓                                                   ▓▓▓▓▓",
                    "▓▓▓▓                           ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓    ▓▓▓▓▓",
                    "▓▓▓▓                           ▓▓▓▓                    ▓▓▓▓▓",
                    "▓▓▓▓                           ▓▓▓▓                    ▓▓▓▓▓",
                    "▓▓▓▓                           ▓▓▓▓                    ▓▓▓▓▓",
                    "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
                    "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓"
                }

            };

            currentMapKey = "Level1";

        }

        public GameMap GetCurrentMap()
        {
            if (maps.ContainsKey(currentMapKey))
            {
                return new GameMap(maps[currentMapKey]);
            }
            throw new Exception($"Map {currentMapKey} not found.");
        }

        public void SetCurrentMap(string mapKey)
        {
            if (maps.ContainsKey(mapKey))
            {
                currentMapKey = mapKey;
            }
            else
            {
                throw new Exception($"Map {mapKey} not found.");
            }
        }

        public GameMap GetMap(string mapKey)
        {
            if (maps.ContainsKey(mapKey))
            {
                return new GameMap(maps[mapKey]);
            }
            throw new Exception($"Map {mapKey} not found.");
        }
    }
}

