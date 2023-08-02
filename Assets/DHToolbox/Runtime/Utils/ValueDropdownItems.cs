using System;
using System.Collections;
using System.Linq;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace DHToolbox.Runtime.Utils
{
    public static class ValueDropdownItems
    {
#if ODIN_INSPECTOR
        public static IEnumerable GetSubclassesOf(Type parent)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => parent.IsAssignableFrom(type) && type.IsClass)
                .Select(
                    type => new ValueDropdownItem<string>(type.Name, type.AssemblyQualifiedName)
                );
        }

        public static IEnumerable GetSubclassesOf(Type parent, Predicate<Type> additionalCheck)
        {
            additionalCheck ??= (_ => true);

            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(
                    type => parent.IsAssignableFrom(type) && type.IsClass && additionalCheck(type)
                )
                .Select(
                    type => new ValueDropdownItem<string>(type.Name, type.AssemblyQualifiedName)
                );
        }
#endif
    }
}
