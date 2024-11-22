using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    void Update()
    {
        // 나의 앞방향을 카메라의 앞방향 설정
        transform.forward = Camera.main.transform.forward;
    }
}
