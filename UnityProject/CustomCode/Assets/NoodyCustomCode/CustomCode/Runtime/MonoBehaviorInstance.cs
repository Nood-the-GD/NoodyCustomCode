using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOOD
{
    public class MonoBehaviorInstance<T> : AbstractMonoBehaviour where T : MonoBehaviour
    {
        private static object lockObject = new object();

        private static T instance;
        public static T Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = (T)FindObjectOfType(typeof(T));
                    }

                    if (instance == null)
                    {
                        Debug.Log("Errorrrrr: " + typeof(T) + " not exit");
                    }

                    return instance;
                }
            }
        }
    }
}

