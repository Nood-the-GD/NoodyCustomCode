using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using NOOD.Extension;
using NOOD.Sound;
using PlasticPipe.PlasticProtocol.Messages;

namespace NOOD.Data
{
    public class DataManager<T> where T : new()
    {
        private static T data;

        // Get Instance of data

        /// <summary>
        /// Only return if Data is saved with QuickSave else use LoadData(filePath) instead
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
        public static T LoadDataFromFile(string filePath)
        {
            string jsonStr = FileExtension.ReadFile(filePath);
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
        public static T LoadDataFromDefaultFile(string fileName, string extension)
        {
            string jsonStr = FileExtension.ReadFile(Path.Combine(Application.dataPath, "Datas", fileName + extension));
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

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
        /// Save to Assets/Datas folder
        /// </summary>
        /// <param name="data"></param>
        public static void SaveToDefaultFolder(T data, string fileName, string extension)
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
            string path = Application.dataPath;
            string fleName = fileName;
            string finalPath = Path.Combine(path, "Datas", fleName + extension);

            FileExtension.WriteToFile(finalPath, jsonString);
        }

        // Return the data to default value
        public static void Clear()
        {
            data = default;
        }
    }
}
