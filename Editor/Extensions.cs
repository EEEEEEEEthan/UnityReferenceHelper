using UnityEngine;

namespace ReferenceHelper.Editor
{
    internal static class Extensions
    {
        public static Transform Find(this Transform @this, string path, bool includeInactive = false)
        {
            var pathParts = path.Split('/');
            var current = @this;
            foreach (var part in pathParts)
            {
                var child = current.Find(part);
                if (child)
                    current = child;
                else
                    return null;
            }

            return current;
        }
    }
}
