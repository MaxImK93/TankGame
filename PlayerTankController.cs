using System;
namespace Tanks
{
	internal class PlayerTankController
	{
        private EntityManager entityManager;
        private Tank playerTank;

        public PlayerTankController(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        public Tank GetPlayerTank()
        {
            foreach (var entity in entityManager.GetEntities())
            {
                if (entity is Tank tank && tank.Type == Tank.TankType.Player)
                {
                    return tank;
                }
            }
            return null;
        }

        public void SetPlayerTank(Tank playerTank)
        {
            this.playerTank = playerTank;
        }

        public void ShootPlayerTank()
        {
            Tank playerTank = GetPlayerTank();
            if (playerTank != null)
            {
                playerTank.Shoot();
            }
        }

        public void MovePlayerTank()
        {
            Tank playerTank = GetPlayerTank();
            if (playerTank != null)
            {
                playerTank.Move();
            }
        }

    }
}

