using System.Collections.Generic;

namespace SmartHospital.Common.ScriptExtensions {
    public static class CollectionExtension {
        public static bool IsEmpty(this ICollection<object> collection) {
            return collection.Count == 0;
        }
    }
}