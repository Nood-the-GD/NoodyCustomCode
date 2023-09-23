using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NOOD.NoodCustomEditor;

namespace NOOD.Tools
{
    public class CreateClassTool 
    {
        [MenuItem("Tools/NoodClassCreator")]
        static void NoodClassCreator()
        {
            string currentDirectory = "Scripts/Game";
            ClassCreatorEditor.CreateWindow(currentDirectory, true);
        }
    }

}
