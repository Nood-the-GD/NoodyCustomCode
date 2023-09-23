using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD.UI;

namespace NOOD.NoodCustomEditor
{
    [Serializable]
    public class TemplateUsingParams 
    {
        public bool isUI = true;
        public string nameSpace = "";
        public string baseName = "";
        public string className => isUI ? "UI" + baseName : baseName;
        public string prefabPath 
        {
            get 
            {
                if (!nameSpace.Contains("."))
                {
                    return "Prefab/" + className;
                }
                return "Prefab/" + nameSpace.Replace(".", "/") + "/" + className;
            }
        }
        public string classPath
        {
            get
            {
                if(!nameSpace.Contains("."))
                {
                    return "";
                }
                return $"{prefabPath.Replace("Prefab/Game", "")}".Replace(className, "");
            }
        }
        public bool hasRootCanvas = true;
        public bool hasPrefab = true;
        public bool hasContext;
    }

    public class ViewTemplate
    {
        public static string Create(TemplateUsingParams parameters)
        {
            TemplateUsingParams p = parameters;

            string text = "";

            text += "using System; \n";
            text += "using System.Collections.Generic; \n";
            text += "using System.Collections; \n";
            text += "using UnityEngine; \n";
            text += "using NOOD; \n";
            text += "using NOOD.UI; \n";

            text += "\n";

            text += "namespace " + p.nameSpace + "\n";
            text += "{\n";
            if (p.isUI == false)
                text += "\tpublic class " + p.className + ": MonoBehaviour \n";
            else
                text += $"\tpublic class {p.className} : {nameof(NoodUI)} \n";
            
            text += "\t{\n";

            text += "\t\tpublic static " + p.className + " Create(Transform parent = null)\n";
            text += "\t\t{\n";
            text += "\t\t\treturn Instantiate(Resources.Load<" + p.className + ">(\"" + p.prefabPath + "\"), parent);\n";
            text += "\t\t}\n";

            text += "\t}\n";
            text += "}\n";

            return text;
        }
    }
}