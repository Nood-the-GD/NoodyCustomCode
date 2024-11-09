using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NOOD
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ButtonAttributeDrawer : Editor
    {
        private GUILayoutOption[] _buttonStyle;
        private ButtonType _cacheButtonType;

        public override void OnInspectorGUI()
        {
            // Draw the default inspector
            DrawDefaultInspector();

            // Get all methods with the Button attribute
            var methods = target.GetType().GetMethods(
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var buttonAttribute = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));
                if (buttonAttribute != null)
                {
                    // Use the button label from the attribute or fall back to the method name
                    string buttonLabel = buttonAttribute.label ?? method.Name;

                    if (GUILayout.Button(buttonLabel, GetButtonStyle(buttonAttribute.buttonType)))
                    {
                        method.Invoke(target, null);
                    }
                }
            }
        }

        private GUILayoutOption[] GetButtonStyle(ButtonType buttonType)
        {
            if (_buttonStyle == null || _cacheButtonType != buttonType)
            {
                switch (buttonType)
                {
                    case ButtonType.Mini:
                        _buttonStyle = new GUILayoutOption[] { GUILayout.Height(20) };
                        break;
                    case ButtonType.Medium:
                        _buttonStyle = new GUILayoutOption[] { GUILayout.Height(30) };
                        break;
                    case ButtonType.Large:
                        _buttonStyle = new GUILayoutOption[] { GUILayout.Height(50) };
                        break;
                }
                _cacheButtonType = buttonType;
            }

            return _buttonStyle;
        }
    }
}
