using System;

namespace USchedule.Models.Domain.Base
{
    public interface IModel
    {
        Guid Id { get; set; }
    }
}