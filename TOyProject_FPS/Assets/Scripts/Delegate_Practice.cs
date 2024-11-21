using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Delegate_Practice : MonoBehaviour
{
    public delegate void MyDelegate(string message);

    int a = 10;

    void PrintMessage(string message)
    {
        print(message);
    }

    void Practice()
    {
        MyDelegate myDelegate = PrintMessage; // 함수 내에서 선언과 초기화
        myDelegate("안녕하세요");
    }

    public Action<string> myAction;

    void Practice_2()
    {
        myAction = PrintMessage;
        myAction("안녕하세요");
    }
}
