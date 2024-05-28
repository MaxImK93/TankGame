using System;
using System.Threading.Tasks;
using static Tanks.TankGemplayState;

namespace Tanks
{
    internal class EnemyTankLogic
    {
        private Tank enemyTank;
        private Random random;
        private GameMap gameMap;
        private List<IGameEntity> entities;

        public EnemyTankLogic(Tank tank, List<IGameEntity> entities)
        {
            enemyTank = tank;
            random = new Random();
            gameMap = new GameMap();
            this.entities = entities;
        }

        public void Update(float deltaTime)
        {
            Cell newPosition = TankGemplayState.ShiftTo(enemyTank.Position, enemyTank.CurrentDir);
              if (enemyTank.CanMoveTo(newPosition, entities))
            {
                enemyTank.Move(entities);
                
            }
            else
            {
                ChangeDirection();
                enemyTank.Move(entities);
            }
        }

        private void ChangeDirection()
        {
            var directions = Enum.GetValues(typeof(SnakeDir));
            SnakeDir newDirection;
            do
            {
                newDirection = (SnakeDir)directions.GetValue(random.Next(directions.Length));
            } while (newDirection == enemyTank.CurrentDir); 

            enemyTank.CurrentDir = newDirection;
        }
    }
}

