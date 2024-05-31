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
                    Console.WriteLine($"Сущность добавлена: {entity.GetType().Name}");
                }
                entitiesToAdd.Clear();

                foreach (var entity in entitiesToRemove)
                {
                    entities.Remove(entity);
                    Console.WriteLine($"Сущность удалена: {entity.GetType().Name}");
                }
                entitiesToRemove.Clear();
            }
        }

        public void AddEntity(IGameEntity entity)
        {
            entitiesToAdd.Add(entity);
            Console.WriteLine($"Добавление сущности: {entity.GetType().Name}");
        }

        public void RemoveEntity(IGameEntity entity)
        {
            entitiesToRemove.Add(entity);
            Console.WriteLine($"Удаление сущности: {entity.GetType().Name}");
        }

        public List<IGameEntity> GetEntities()
        {
            return entities;
        }

        public void Clear()
        {
            entities.Clear();
            entitiesToAdd.Clear();
            entitiesToRemove.Clear();
            Console.WriteLine("Все сущности очищены");
        }
    }
}

