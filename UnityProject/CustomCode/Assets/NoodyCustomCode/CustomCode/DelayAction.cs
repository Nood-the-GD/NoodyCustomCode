using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace NOOD
{
    public class DelayAction : MonoBehaviour
    {
        private static GameObject _initObject;
        private static List<DelayAction> _delayActions;
        public Action OnComplete;
        [SerializeField] private float delaySecond;
        [SerializeField] private Action delayAction;
        [SerializeField] private string functionName;
        public bool isStop;
        private CancellationTokenSource _cancellationTokenSource;

        #region Static
        public static DelayAction Create()
        {
            if(_initObject == null || _initObject.gameObject == null)
            {
                _initObject = new GameObject("DelayInitObject");
                _delayActions = new List<DelayAction>();
                DontDestroyOnLoad(_initObject);
            }
            GameObject delayObject = new GameObject("DelayObject");
            DelayAction delayAction = delayObject.AddComponent<DelayAction>();
            _delayActions.Add(delayAction);
            DontDestroyOnLoad(delayObject);

            delayAction.isStop = false;
            return delayAction;
        }
        public static void StopDelayFunction(string functionName)
        {
            if(_delayActions != null && _delayActions.Any(x => x.functionName == functionName))
            {
                DelayAction delayAction = _delayActions.First(x => x.functionName == functionName);
                _delayActions.Remove(delayAction);
                delayAction.StopDelayFunction();
            }
        }
        #endregion
        private async void DelayFunction()
        {
            try
            {
                await UniTask.Delay((int)(delaySecond * 1000f), cancellationToken: _cancellationTokenSource.Token);
                if (isStop) 
                {
                    Destroy(this.gameObject);
                    return;
                }
                delayAction?.Invoke();
                OnComplete?.Invoke();
                _delayActions.Remove(this);
                Destroy(this.gameObject);
            }
            catch(OperationCanceledException)
            {
                Debug.Log($"Cancel {functionName} successfully");
                Destroy(this.gameObject);
                return;
            }

        }

        public void StartDelayFunction(Action action, string functionName, float second)
        {
            this.delayAction = action;
            this.delaySecond = second;
            this.functionName = functionName;
            _cancellationTokenSource = new CancellationTokenSource(); 
            DelayFunction();
        }
        private void StopDelayFunction()
        {
            Debug.Log("Stop");
            isStop = true;
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}
