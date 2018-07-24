using System;

namespace USchedule.Models.DTO
{
    public class LocatorParameter
    {
        public LocatorParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public LocatorParameter(Type type, object value)
        {
            Type = type;
            Value = value;
        }
        public string Name { get; set; }
        public Type Type { get; set; }
        public object Value { get; set; }
    }
}