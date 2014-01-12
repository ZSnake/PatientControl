using System;

namespace ClassLibrary1
{
    public class Room : IEntity
    {
        public virtual Guid Id { get; set; }

        public virtual String Name { get; set; }
    }
}