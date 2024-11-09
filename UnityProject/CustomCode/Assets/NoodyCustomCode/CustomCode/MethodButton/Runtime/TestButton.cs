using System.Collections;
using System.Collections.Generic;
using NOOD;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    [Button(buttonType: ButtonType.Medium)]
    public void TestMethod()
    {
        Debug.Log("Test method called");
    }

    [Button("Test Method 2", ButtonType.Mini)]
    public void TestMethod2()
    {
        Debug.Log("Test method 2 called");
    }
}
