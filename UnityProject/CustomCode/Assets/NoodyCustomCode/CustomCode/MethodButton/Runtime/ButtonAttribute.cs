using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOOD
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : PropertyAttribute
    {
        public ButtonAttribute(string label = null, ButtonType buttonType = ButtonType.Mini)
        {
            this.label = label;
            this.buttonType = buttonType;
        }

        public string label;
        public ButtonType buttonType;
    }

    public enum ButtonType
    {
        Mini,
        Medium,
        Large
    }
}
