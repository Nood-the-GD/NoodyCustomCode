using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace NOOD
{
    public class CoroutineScript : MonoBehaviour
    {
        private static GameObject _initObject;
        private static List<CoroutineScript> _coroutineScripts;
        private Func<bool> func;
        private Action onCompleteLoop;
        private bool isComplete;
        private float pauseTimePerLoop;
        private int loopTime = -1;
        private int performTime;
        private string functionName;

        #region Static
        public static CoroutineScript Create()
        {
            if(_initObject == null || _initObject.gameObject == null)
            {
                _coroutineScripts = new List<CoroutineScript>();
                _initObject = new GameObject("CR_InitObject");
            }
            GameObject gameObject = new GameObject("CoroutineScriptObject");
            CoroutineScript coroutineScript = gameObject.AddComponent<CoroutineScript>();
            _coroutineScripts.Add(coroutineScript);
            return coroutineScript;
        }
        public static void StopCoroutineLoop(string functionName)
        {
            if(_coroutineScripts != null && _coroutineScripts.Any(x => x.functionName == functionName))
            {
                CoroutineScript coroutineScript = _coroutineScripts.First(x => x.functionName == functionName);
                _coroutineScripts.Remove(coroutineScript);
                coroutineScript.StopCoroutineSloop();
            }
        }
        #endregion

        public void StartDelayLoop(Func<bool> func, string functionName, float pauseTimePerLoop, int loopTime)
        {
            isComplete = false;
            this.func = func;
            this.pauseTimePerLoop = pauseTimePerLoop;
            this.loopTime = loopTime;
            this.performTime = 0;
            LoopFunctionDelay();
        }
        public void StopCoroutineSloop()
        {
            isComplete = true;
            Destroy(this.gameObject, 0.2f);
        }

        public void Complete()
        {
            isComplete = true; 
            onCompleteLoop?.Invoke();
            _coroutineScripts.Remove(this);
            Destroy(this.gameObject, 0.2f);
        }
        
        async void LoopFunctionDelay()
        {
            while( isComplete == false)
            {
                await UniTask.Delay((int)(pauseTimePerLoop * 1000));
                performTime++;
                if(performTime == loopTime || func?.Invoke() == true)
                {
                    isComplete = true;
                    Complete();
                }
            }
        }
    }
}
