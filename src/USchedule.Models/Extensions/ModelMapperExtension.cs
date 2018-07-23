using AutoMapper;
using USchedule.Models.Domain;
using USchedule.Models.Domain.Base;

namespace USchedule.Models.Extensions
{
    public static class ModelMapperExtension
    {
        public static TTo To<TTo>(this IModel model) where TTo : class
        {
            return Mapper.Map<TTo>(model);
        }
    }
}
