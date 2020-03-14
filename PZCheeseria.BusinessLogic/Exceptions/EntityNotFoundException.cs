using System;

namespace PZCheeseria.BusinessLogic.Exceptions
{
    public class EntityNotFoundException:Exception
    {
        public EntityNotFoundException(string entityName, object identifier)
            :base($"Entity {entityName} with identifier: {identifier} not found")
        {
            EntityName = entityName;
            EntityKey = identifier;

        }

        public object EntityKey { get; }

        public string EntityName { get; }
    }
}