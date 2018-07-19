using System.Collections.Generic;
using AutoMapper;
using USchedule.Core.Entities.Abstractions;
using USchedule.Models.Domain;

namespace USchedule.Models.Extensions
{
    public static class EntityMapperExtension
    {
        public static TTo To<TTo>(this IEntity entity)
        {
            return Mapper.Map<TTo>(entity);
        }

        public static IList<TTo> To<TTo>(this IEnumerable<IEntity> entities) where TTo : class, IModel
        {
            return Mapper.Map<IList<TTo>>(entities);
        }
    }
}