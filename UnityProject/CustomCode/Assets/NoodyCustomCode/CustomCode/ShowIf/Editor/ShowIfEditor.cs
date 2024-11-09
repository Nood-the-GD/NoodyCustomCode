using System;
using UnityEditor;
using UnityEngine;

namespace NOOD
{
    [CustomPropertyDrawer(typeof(ShowIf))]
    public class ShowIfEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIf showIf = attribute as ShowIf;

            // Get the target bool field/property
            var targetProperty = property.serializedObject.FindProperty(showIf.target);

            if (targetProperty == null)
            {
                EditorGUI.LabelField(position, label.text, "Target property not found");
                return;
            }

            // Only draw if the target bool matches the desired condition
            if (GetCondition(targetProperty, showIf))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIf showIf = attribute as ShowIf;
            var targetProperty = property.serializedObject.FindProperty(showIf.target);

            if (targetProperty == null || !GetCondition(targetProperty, showIf))
            {
                return 0;
            }

            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        private bool GetCondition(SerializedProperty property, ShowIf showIf)
        {
            var targetProperty = property.serializedObject.FindProperty(showIf.target);
            if (targetProperty != null)
            {
                switch (targetProperty.propertyType)
                {
                    case SerializedPropertyType.Boolean:
                        if (showIf.condition is bool)
                            return targetProperty.boolValue == (bool)showIf.condition;
                        else
                            Debug.LogWarning($"ShowIf condition is not a boolean: {showIf.condition}");
                        break;
                    case SerializedPropertyType.Enum:
                        if (showIf.condition is Enum)
                            return targetProperty.enumValueIndex == (int)showIf.condition;
                        else
                            Debug.LogWarning($"ShowIf condition is not an enum: {showIf.condition}");
                        break;
                }
            }
            return false;
        }
    }
}
