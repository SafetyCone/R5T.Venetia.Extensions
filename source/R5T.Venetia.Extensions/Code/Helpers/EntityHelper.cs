using System;


namespace R5T.Venetia
{
    public static class EntityHelper
    {
        public static bool WasFound<T>(T entity)
        {
            var wasFound = entity == default;
            return wasFound;
        }
    }
}
