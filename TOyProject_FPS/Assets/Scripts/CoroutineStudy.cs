using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutineStudy : MonoBehaviour
{
    // 반환자료형이 없는 함수를 담을 변수
    Action action;

    // 반환 자료형이  존재하는 함수를 담을 변수
    Func<int> func;

    Func<int, string> func2; // 앞에는 매개변수 맨 뒤가 매개변수;
    void Start()
    {
        // 직접 함수 넣는 법
        action = ActionTest;
        action();

        // Action에 람다식으로 넣는 법
        action = () => { print("반환 자료형이 void이고 매개변수가 없음");  };

        // Func에 직접 함수를 넣는 법
        func = FuncTest;

        func();


        int result = func();

        // Fun에 람다식으로 넣게
        func = () => { return 1; };
    }

    void Update()
    {
        
    }

    void ActionTest()
    {
        print("반환 자료형이 void이고 매개변수가 없음");
    }

    int FuncTest()
    {
        return 1;
    }
}
