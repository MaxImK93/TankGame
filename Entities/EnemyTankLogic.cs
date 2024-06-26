﻿using System;
using System.Collections.Generic;
using Tanks.Entities;
using Tanks.Core;
using Tanks.Managers;
using Tanks.Controllers;
using static Tanks.TanksLogic.TankGemplayState;
using Tanks.Interfaces;

namespace Tanks.TanksLogic
{
    internal class EnemyTankLogic
    {
        private Tank enemyTank;
        private Random random;
        private GameMap gameMap;
        private EntityManager entityManager;

        private float shootInterval = 0.1f;
        private float timeSinceLastShot = 0.0f;

        private TankGemplayState gameState;
        private MapManager mapManager;

        public EnemyTankLogic(Tank tank, List<IGameEntity> entities, EntityManager entityManager, MapManager mapManager)
        {
            enemyTank = tank;
            random = new Random();
            this.entityManager = entityManager;
            this.mapManager = mapManager;
            gameMap = mapManager.GetCurrentMap();
        }

        public void Update(float deltaTime)
        {
            timeSinceLastShot += deltaTime;

            if (CanSeePlayer())
            {
                if (timeSinceLastShot >= shootInterval)
                {
                    Shoot();
                    timeSinceLastShot = 0.0f;
                }
            }
            else
            {
                Cell newPosition = TankGemplayState.ShiftTo(enemyTank.Position, enemyTank.CurrentDir);
                if (enemyTank.CanMoveTo(newPosition, entityManager.GetEntities()))
                {
                    enemyTank.Move();

                }
                else
                {
                    ChangeDirection();
                    enemyTank.Move();
                }
            }
        }

        private void ChangeDirection()
        {
            var directions = Enum.GetValues(typeof(TankGemplayState.TankDir));
            TankGemplayState.TankDir newDirection;
            do
            {
                newDirection = (TankGemplayState.TankDir)directions.GetValue(random.Next(directions.Length));
            } while (newDirection == enemyTank.CurrentDir);

            enemyTank.CurrentDir = newDirection;
        }

        private void Shoot()
        {
            enemyTank.Shoot();
        }

        private bool CanSeePlayer()
        {
            var direction = enemyTank.CurrentDir;
            var position = enemyTank.Position;

            while (true)
            {
                position = TankGemplayState.ShiftTo(position, direction);

                if (position._X < 0 || position._X >= gameMap.Width || position._Y < 0 || position._Y >= gameMap.Height)
                {
                    return false;
                }

                var obstacle = gameMap.GetObstacleType(position._X, position._Y, entityManager.GetEntities(), enemyTank);

                if (obstacle == GameMap.ObstacleType.Wall || obstacle == GameMap.ObstacleType.DamagedWall)
                {
                    return false;
                }

                if (obstacle == GameMap.ObstacleType.Tank)
                {
                    foreach (var entity in entityManager.GetEntities())
                    {
                        if (entity is Tank playerTank && playerTank.Type == Tank.TankType.Player)
                        {
                            if (playerTank.Position._X == position._X && playerTank.Position._Y == position._Y)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }

        public bool IsTankAlive()
        {
            return enemyTank.IsAlive;
        }
    }
}
