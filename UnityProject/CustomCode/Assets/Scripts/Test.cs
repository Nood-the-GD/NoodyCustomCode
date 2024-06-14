using System.Collections;
using System.Collections.Generic;
using NOOD;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        NoodyCustomCode.StartDelayFunction(() =>
        {
            Debug.Log("Delay after 2 second");
        }, 2f);
        NoodyCustomCode.StartDelayFunction(() =>
        {
            Debug.Log("Delay after 3 second");
        },"DelayThreeSecond" ,3f);
        NoodyCustomCode.StartDelayFunction(() =>
        {
            NoodyCustomCode.StopDelayFunction("DelayThreeSecond");
        },1f);
    }
}
