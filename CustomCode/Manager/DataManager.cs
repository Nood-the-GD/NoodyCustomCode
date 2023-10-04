using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using NOOD.Extension;

namespace NOOD.Data
{
    public class DataManager<T> where T : new()
    {
        private static T data;

        // Get Instance of data

        /// <summary>
        /// Only return if Data is saved with QuickSave else use LoadData<>(filePath) instead
        /// </summary> 
        /// <value></value>
        public static T Data
        {
            get
            {
                if(data == null)
                {
                    LoadData();
                }
                return data;
            }
        }

#region LoadData
        // Load the json on disk and convert to T
        private static void LoadData()
        {
            string objString = PlayerPrefs.GetString(typeof(T).ToString(), "");
            if(objString == "")
            {
                data = new T();
                QuickSave();
            }
            else
            {
                data = JsonUtility.FromJson<T>(objString);
            }
        }
        public static T LoadDataFromFile(string filePath, string extension)
        {
            string jsonStr = FileExtension.ReadFile(filePath, extension);
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
        /// <summary>
        /// Load the data from Resources/Datas
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static T LoadDataFromDefaultFile(string fileName)
        {
            if (FileExtension.IsExitFileInDefaultFolder(fileName))
            {
                string jsonStr = Resources.Load<TextAsset>(Path.Combine("Datas", fileName)).text;
                return JsonConvert.DeserializeObject<T>(jsonStr);
            }
            Debug.LogWarning("Not exist file name " + fileName + " in default folder");
            return default;
        }
#endregion

#region SaveData
        // Save the data to disk
        /// <summary>
        /// Save to PlayerPrefs
        /// </summary>
        public static void QuickSave()
        {
            PlayerPrefs.SetString(typeof(T).ToString(), JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }
        public static void SaveToFile(string filePath, T data)
        {
            string jsonString = JsonConvert.SerializeObject(data);

        }
        /// <summary>
        /// Save to Assets/Resources/Datas folder
        /// </summary>
        /// <param name="data"></param>
        public static void SaveToDefaultFolder(T data, string fileName, string extension)
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
            string path = Application.persistentDataPath;
            string fleName = fileName;
            string finalPath = Path.Combine(path, "Datas", fleName + extension);
            Debug.Log(path);
#if UNITY_EDITOR
            FileExtension.WriteToFile(finalPath, jsonString);
#endif
        }
#endregion

        // Return the data to default value
        public static void Clear()
        {
            data = default;
        }
    }
}
