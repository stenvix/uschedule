using System;

namespace USchedule.Core.Entities.Abstractions
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}