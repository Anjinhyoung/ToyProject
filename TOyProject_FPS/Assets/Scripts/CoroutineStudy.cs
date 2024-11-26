using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutineStudy : MonoBehaviour
{
    // ��ȯ�ڷ����� ���� �Լ��� ���� ����
    Action action;

    // ��ȯ �ڷ�����  �����ϴ� �Լ��� ���� ����
    Func<int> func;

    Func<int, string> func2; // �տ��� �Ű����� �� �ڰ� �Ű�����;
    void Start()
    {
        // ���� �Լ� �ִ� ��
        action = ActionTest;
        action();

        // Action�� ���ٽ����� �ִ� ��
        action = () => { print("��ȯ �ڷ����� void�̰� �Ű������� ����");  };

        // Func�� ���� �Լ��� �ִ� ��
        func = FuncTest;

        func();


        int result = func();

        // Fun�� ���ٽ����� �ְ�
        func = () => { return 1; };
    }

    void Update()
    {
        
    }

    void ActionTest()
    {
        print("��ȯ �ڷ����� void�̰� �Ű������� ����");
    }

    int FuncTest()
    {
        return 1;
    }
}
