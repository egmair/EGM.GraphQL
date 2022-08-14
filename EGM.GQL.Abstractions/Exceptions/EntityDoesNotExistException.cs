using System;

namespace EGM.GQL.Abstractions.Exceptions
{
    public sealed class EntityDoesNotExistException<T> : Exception
    {
        public Type EntityType { get; }
        
        public EntityDoesNotExistException(string message) : base(message)
        {
            EntityType = typeof(T);
        }
    }
}