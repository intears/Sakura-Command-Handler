using System.Reflection;

namespace SakuraCommandHandler.Parameter
{
    public class CommandParameter : ParameterAttribute
    {
        public CommandParameter(ParameterAttribute attr, PropertyInfo property) : base(attr.Name, attr.Desc, attr.Optional)
        {
            Property = property;
        }

        public PropertyInfo Property { get; private set; }
    }
}
