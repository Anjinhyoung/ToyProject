using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    void Update()
    {
        // ���� �չ����� ī�޶��� �չ��� ����
        transform.forward = Camera.main.transform.forward;
    }
}
