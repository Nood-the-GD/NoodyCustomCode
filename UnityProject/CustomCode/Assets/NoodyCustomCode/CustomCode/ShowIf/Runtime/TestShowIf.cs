using NOOD;
using UnityEngine;

public class TestShowIf : MonoBehaviour
{
    public bool test;

    [ShowIf("test", true)]
    [SerializeField] private string _thisShowWhenTestIsTrue = "This is true";

    [SerializeField] private TestEnum _testEnum;

    [ShowIf("_testEnum", TestEnum.Test1)]
    [SerializeField] private string _thisShowWhenTestEnumIsTest1 = "Enum 1";

    [ShowIf("_testEnum", TestEnum.Test2)]
    [SerializeField] private string _thisShowWhenTestEnumIsTest2 = "Enum 2";
}


public enum TestEnum
{
    Test1,
    Test2,
}
