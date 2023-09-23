using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;

namespace NOOD.Extension
{
    public static class EnumExtension
    {
        public static T AddToEnum<T>(this T sourceEnum, string value, T defaultValue) where T : struct
        {
            if(string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            T result;
            return Enum.TryParse<T>(value, out result) ? result : defaultValue;
        }
    }

    public static class StringExtension
    {
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            T result;
            return Enum.TryParse<T>(value, out result) ? result : defaultValue;
        }
    }

    public static class EnumCreator
    {
        const string extension = ".cs";
        public static void WriteToEnum<T>(string enumFolderPath, string enumFileName, ICollection<string> data)
        {
            using (StreamWriter file = File.CreateText(enumFolderPath + enumFileName + extension))
            {
                file.WriteLine("public enum " + typeof(T).Name + "\n{ ");

                int i = 0;
                foreach (var line in data)
                {
                    string lineRep = line.ToString().Replace(" ", string.Empty);
                    if (!string.IsNullOrEmpty(lineRep))
                    {
                        file.WriteLine(string.Format("	{0} = {1},",
                            lineRep, i));
                        i++;
                    }
                }

                file.WriteLine("}");
                AssetDatabase.ImportAsset(enumFolderPath + enumFileName + extension);
            }
        }
    }

    public static class RootPathExtension<T> 
    {
        public static string RootPath
        {
            get 
            {
                var g = AssetDatabase.FindAssets($"t:Script {typeof(T).Name}");
                return AssetDatabase.GUIDToAssetPath(g[0]);
            }
        }
    }
}
