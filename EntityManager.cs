using System;
namespace Tanks
{
	internal class EntityManager
	{
        private List<IGameEntity> entities;
        private List<IGameEntity> entitiesToAdd;
        private List<IGameEntity> entitiesToRemove;

        public EntityManager()
        {
            entities = new List<IGameEntity>();
            entitiesToAdd = new List<IGameEntity>();
            entitiesToRemove = new List<IGameEntity>();
        }

        public void UpdateEntities(float deltaTime)
        {
            foreach (var entity in entities)
            {
                entity.Update(deltaTime);
            }
        }

        public void ProcessEntityChanges()
        {
            if (entitiesToAdd.Count > 0 || entitiesToRemove.Count > 0)
            {
                foreach (var entity in entitiesToAdd)
                {
                    entities.Add(entity);
                }
                entitiesToAdd.Clear();

                foreach (var entity in entitiesToRemove)
                {
                    entities.Remove(entity);
                }
                entitiesToRemove.Clear();
            }
        }

        public void AddEntity(IGameEntity entity)
        {
            entitiesToAdd.Add(entity);
        }

        public void RemoveEntity(IGameEntity entity)
        {
            entitiesToRemove.Add(entity);
        }

        public List<IGameEntity> GetEntities()
        {
            return entities;
        }
    }
}

