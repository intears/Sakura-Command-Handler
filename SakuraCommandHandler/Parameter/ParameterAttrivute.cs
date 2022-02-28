using System;

namespace SakuraCommandHandler.Parameter
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterAttribute : Attribute
    {
        public string Name { get; private set; }
        public bool Optional { get; private set; }
        
        public string Desc { get; private set; }
        public ParameterAttribute(string name, string desc = "", bool optional = false)
        {
            Name = name;
            Optional = optional;
            Desc = desc;
        }
    }
}
