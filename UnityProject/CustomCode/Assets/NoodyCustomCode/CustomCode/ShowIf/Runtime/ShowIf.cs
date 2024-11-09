using System;
using UnityEngine;

namespace NOOD
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ShowIf : PropertyAttribute
    {
        public ShowIf(string target, object condition)
        {
            this.target = target;
            this.condition = condition;
        }

        public string target;
        public object condition;
    }
}
