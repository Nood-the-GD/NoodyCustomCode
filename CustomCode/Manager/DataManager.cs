using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOOD.Data
{
    public class DataManager<T> where T : new()
    {
        private static T data;

        // Get Instance of data
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
                Save();
            }
            else
            {
                data = JsonUtility.FromJson<T>(objString);
            }
        }

        // Save the data to disk
        public static void Save()
        {
            PlayerPrefs.SetString(typeof(T).ToString(), JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }

        // Return the data to default value
        public static void Clear()
        {
            data = default;
        }
    }
}
