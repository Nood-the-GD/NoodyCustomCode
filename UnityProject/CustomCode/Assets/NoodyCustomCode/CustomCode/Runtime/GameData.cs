using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NOOD.Extension;
using UnityEngine;
using NOOD.SerializableDictionary;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NOOD
{
    [CreateAssetMenu(fileName = "GameData", menuName = "NOOD/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public SerializableDictionary<string, PrefabPathClass> prefabPathDictionary = new SerializableDictionary<string, PrefabPathClass>();

        #region Instance
        private static GameData _instance;
        public static GameData Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameData[] gameDataArray = Resources.LoadAll<GameData>("");
                    if (gameDataArray != null && gameDataArray.Count() > 0)
                    {
                        _instance = gameDataArray[0];
                        return _instance;
                    }
                    else
                    {
                        Debug.LogError("GameData didn't exist, please create one in Resources folder using Create -> GameData");
                        return null;
                    }
                }
                else
                {
                    return _instance;
                }
            }
        }

        #endregion

        #region Prefabs functions
        public GameObject InstantiatePrefab(PrefabEnum prefabEnum)
        {
            return Instantiate(prefabPathDictionary.Dictionary[prefabEnum.ToString()].gameObject);
        }
        public string GetPrefabPath(PrefabEnum prefabEnum)
        {
            return prefabPathDictionary.Dictionary[prefabEnum.ToString()].pathInResources;
        }
        #endregion

#if UNITY_EDITOR
        private int _dictionaryCountOld;

        private void Awake()
        {
            _dictionaryCountOld = prefabPathDictionary.Count;
        }

        #region Update Asset Path
        // [Button("Refresh Data", ButtonSizes.Large)]
        private void UpdateAssetPath()
        {
            foreach (PrefabPathClass prefabPathClass in prefabPathDictionary.Dictionary.Values)
            {
                string path = AssetDatabase.GetAssetPath(prefabPathClass.gameObject);
                string resourcesPath = path.Split("Resources/").Last();
                resourcesPath = resourcesPath.Replace(".prefab", "");
                prefabPathClass.pathInResources = resourcesPath;
            }

            bool isNeedToRefresh = false;
            foreach (string key in prefabPathDictionary.Dictionary.Keys)
            {
                if (PrefabEnum.TryParse(key, out PrefabEnum prefabEnum) == false)
                {
                    isNeedToRefresh = true;
                }
            }

            if (isNeedToRefresh)
            {
                string folderPath = RootPathExtension<GameData>.FolderPath(".cs");
                EnumCreator.WriteToEnum<PrefabEnum>(folderPath, "PrefabEnum", prefabPathDictionary.Dictionary.Keys.ToList(), "NOOD");
                _dictionaryCountOld = prefabPathDictionary.Count;
            }
        }
        #endregion
#endif
    }

    [System.Serializable]
    public class PrefabPathClass
    {
        public GameObject gameObject;
        public string pathInResources;
    }

    public enum PlateType
    {
        DirtyPlate,
        CleanPlate
    }
}
