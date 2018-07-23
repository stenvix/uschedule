using System;

namespace USchedule.Models.Domain.Base
{
    public class BaseModel: IModel
    {
        public Guid Id { get; set; }
    }
}