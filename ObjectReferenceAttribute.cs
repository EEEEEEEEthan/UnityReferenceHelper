using System;
using UnityEngine;

namespace ReferenceHelper
{
    /// <summary>
    ///     Attribute for automatically referencing child object components
    ///     Usage: [ObjectReference("UI/Button")] public Button myButton;
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ObjectReferenceAttribute : PropertyAttribute
    {
        public string Path { get; private set; }

        public ObjectReferenceAttribute(string path = "")
        {
            Path = path ?? string.Empty;
        }
    }
}
