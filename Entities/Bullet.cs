﻿using System;
using static Tanks.Core.GameMap;
using static Tanks.TanksLogic.TankGemplayState;
using Tanks.Core;
using Tanks.Managers;
using Tanks.Rendering;
using Tanks.Interfaces;
using Tanks.TanksLogic;

namespace Tanks.Entities
{
	internal class Bullet : IGameEntity
	{
        public Cell Position { get; private set; }
        public TankDir Direction { get; private set; }

        public bool IsAlive => throw new NotImplementedException();

        private GameMap gameMap;
        private List<IGameEntity> entities;

        private EntityManager entityManager;

        public Bullet(Cell startPosition, TankDir direction, GameMap map, List<IGameEntity> entities, EntityManager entityManager)
        {
            Position = startPosition;
            Direction = direction;
            gameMap = map;
            this.entities = entities;
            this.entityManager = entityManager;
        }

        public void Update(float deltaTime)
        {
            Move();
        }

        public void Draw(ConsoleRenderer renderer)
        {
            if (Position._X >= 0 && Position._X < renderer.width && Position._Y >= 0 && Position._Y < renderer.height)
            {
                renderer.SetPixel(Position._X, Position._Y, '•', 1);
            }
        }

        public void Move()
        {
            Cell newPosition = ShiftTo(Position, Direction);
            var obstacle = gameMap.GetObstacleType(newPosition._X, newPosition._Y, entities, null);

            if (obstacle == ObstacleType.Wall || obstacle == ObstacleType.DamagedWall)
            {
                gameMap.DamageWall(newPosition._X, newPosition._Y);
                entityManager.RemoveEntity(this);
                return;
            }
            else if (obstacle == ObstacleType.Tank)
            {
                entityManager.RemoveEntity(this);
                foreach (var entity in entities)
                {
                    if (entity is Tank tank)
                    {
                        char[,] tankShape = tank.GetTankShape();
                        for (int i = 0; i < tankShape.GetLength(0); i++)
                        {
                            for (int j = 0; j < tankShape.GetLength(1); j++)
                            {
                                int tankX = tank.Position._X + j - 1;
                                int tankY = tank.Position._Y + i - 1;
                                if (newPosition._X == tankX && newPosition._Y == tankY)
                                {
                                    tank.TakeDamage(1);
                                    break;
                                }
                            }
                        }
                    }
                }
                return;
            }

            Position = newPosition;
        }
    }
}

