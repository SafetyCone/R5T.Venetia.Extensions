using System;
using System.Collections.Generic;


namespace R5T.Venetia
{
    public static class EntityHelper
    {
        public static bool WasFound<T>(T entity) where T: class
        {
            var wasFound = entity != default;
            return wasFound;
        }

        public static bool CheckKeyEquality<TAppType, TEntityType, TKey>(
            TAppType appType, Func<TAppType, TKey> appTypeKeySelector,
            TEntityType entityType, Func<TEntityType, TKey> entityTypeKeySelector,
            IEqualityComparer<TKey> keyEqualityComparer)
        {
            var appTypeKey = appTypeKeySelector(appType);
            var entityTypeKey = entityTypeKeySelector(entityType);

            var keysEqual = keyEqualityComparer.Equals(appTypeKey, entityTypeKey);
            return keysEqual;
        }

        public static bool CheckKeyEquality<TAppType, TEntityType, TKey>(
            TAppType appType, Func<TAppType, TKey> appTypeKeySelector,
            TEntityType entityType, Func<TEntityType, TKey> entityTypeKeySelector)
        {
            var equalityComparer = EqualityComparer<TKey>.Default;

            var keysEqual = EntityHelper.CheckKeyEquality(appType, appTypeKeySelector, entityType, entityTypeKeySelector);
            return keysEqual;
        }
    }
}
