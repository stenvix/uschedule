using System;
using USchedule.Core.Entities.Abstractions;

namespace USchedule.Core.Entities.Implementations.Base
{
    public abstract class BaseEntity: IEntity
    {
        public Guid Id { get; set; }
    }
}