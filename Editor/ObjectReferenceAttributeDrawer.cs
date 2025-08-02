#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ReferenceHelper.Editor
{
    [CustomPropertyDrawer(typeof(ObjectReferenceAttribute))]
    internal sealed class ObjectReferenceAttributeDrawer : PropertyDrawer
    {
        private GUIContent _buttonClear;
        private GUIContent _buttonCreate;

        private static void CreateMissingGameObject(MonoBehaviour monoBehaviour, string path)
        {
            var pathParts = path.Split('/');
            var current = monoBehaviour.transform;
            foreach (var part in pathParts)
            {
                var child = current.Find(part);
                if (child)
                {
                    current = child;
                }
                else
                {
                    var newChild = new GameObject(part);
                    Undo.RegisterCreatedObjectUndo(newChild, "Create GameObject");
                    Undo.SetTransformParent(newChild.transform, current, "Set Parent");
                    current = newChild.transform;
                }
            }
        }

        private static void UpdateReference(SerializedProperty property, Object target)
        {
            Undo.RecordObject(property.serializedObject.targetObject, "Update Object Reference");
            property.objectReferenceValue = target;
            property.serializedObject.ApplyModifiedProperties();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            const float buttonWidth = 60f;
            const float spacing = 2f;
            var buttonType = GetRequiredButtonType(property);
            var fieldRect = buttonType == ButtonTypeCode.None
                ? position
                : new Rect(position.x, position.y, position.width - buttonWidth - spacing, position.height);
            var buttonRect = new Rect(position.x + position.width - buttonWidth, position.y, buttonWidth, position.height);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(fieldRect, property, label);
            EditorGUI.EndDisabledGroup();
            var originalColor = GUI.backgroundColor;
            switch (buttonType)
            {
                case ButtonTypeCode.Create:
                {
                    GUI.backgroundColor = Color.red;
                    _buttonCreate = _buttonCreate ?? new GUIContent("Create", "/" + GetPath(property) + "\nMissing reference detected. Click to create.");
                    if (GUI.Button(buttonRect, _buttonCreate)) CreateMissingObjects(property);
                    break;
                }
                case ButtonTypeCode.Clear:
                {
                    GUI.backgroundColor = new Color(1f, 0.7f, 0f);
                    _buttonClear = _buttonClear ?? new GUIContent("Clear", "/" + GetPath(property) + "\nReference mismatch detected. Click to clear.");
                    if (GUI.Button(buttonRect, _buttonClear)) UpdateReference(property, null);
                    break;
                }
                case ButtonTypeCode.None:
                    break;
                default:
                    Debug.LogError("Unknown ButtonType: " + buttonType, property.serializedObject.targetObject);
                    break;
            }

            GUI.backgroundColor = originalColor;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        private ButtonTypeCode GetRequiredButtonType(SerializedProperty property)
        {
            var monoBehaviour = property.serializedObject.targetObject as MonoBehaviour;
            if (!monoBehaviour) return ButtonTypeCode.None;
            var currentValue = property.objectReferenceValue;
            var target = GetTarget(property);
            if (currentValue == null) return ButtonTypeCode.Create;
            if (target && currentValue == target) return ButtonTypeCode.None;
            return ButtonTypeCode.Clear;
        }

        private void CreateMissingObjects(SerializedProperty property)
        {
            var monoBehaviour = property.serializedObject.targetObject as MonoBehaviour;
            if (!monoBehaviour) return;
            var attr = (ObjectReferenceAttribute)attribute;
            var fieldType = fieldInfo.FieldType;
            CreateMissingGameObject(monoBehaviour, attr.Path);
            var targetGameObject = monoBehaviour.transform.Find(attr.Path, true)?.gameObject;
            if (!targetGameObject) return;
            Object targetObject = null;
            if (fieldType == typeof(GameObject))
            {
                targetObject = targetGameObject;
            }
            else if (typeof(Component).IsAssignableFrom(fieldType))
            {
                var component = targetGameObject.GetComponent(fieldType);
                if (!component) component = Undo.AddComponent(targetGameObject, fieldType);
                targetObject = component;
            }

            UpdateReference(property, targetObject);
            EditorGUIUtility.PingObject(targetObject);
        }

        private Object GetTarget(SerializedProperty property)
        {
            if (!(property.serializedObject.targetObject is MonoBehaviour monoBehaviour)) return null;
            var target = monoBehaviour.transform.Find(GetPath(property), true);
            if (!target) return null;
            var targetGameObject = target.gameObject;
            switch (fieldInfo.FieldType)
            {
                case var type when type == typeof(GameObject):
                    return targetGameObject;
                case var type when typeof(Component).IsAssignableFrom(type):
                    return targetGameObject.GetComponent(type);
                default:
                    return null;
            }
        }

        private string GetPath(SerializedProperty property)
        {
            var monoBehaviour = property.serializedObject.targetObject as MonoBehaviour;
            if (!monoBehaviour) return null;
            var attr = (ObjectReferenceAttribute)attribute;
            return attr.Path;
        }

        private enum ButtonTypeCode
        {
            None = 0,
            Clear = 1,
            Create = 2
        }
    }
}
#endif
