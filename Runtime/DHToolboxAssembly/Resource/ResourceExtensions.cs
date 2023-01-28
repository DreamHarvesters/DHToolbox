using Foundations.Scripts.AmountOwner;

namespace Foundations.Scripts.Resource
{
    public static class ResourceExtensions
    {
        public static void Add(this IIntReactiveNumber resource, int amount) => resource.Set(resource.Current + amount);
        public static void Remove(this IIntReactiveNumber resource, int amount) => resource.Set(resource.Current - amount);

        public static void Add(this IFloatReactiveNumber resource, float amount) =>
            resource.Set(resource.Current + amount);

        public static void Remove(this IFloatReactiveNumber resource, float amount) =>
            resource.Set(resource.Current - amount);
    }
}