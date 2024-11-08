using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    // 회전 속력
    public float rotSpeed = 200;
    // 회전 값 (각도는 누적하기)
    float rotY;
    float rotX;

    void Start()
    {
        
    }

    void Update()
    {
        // 마우스 움직임 값을  받아오자
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // 회전한 각도를 누적시킨다.
        rotY += mx * rotSpeed * Time.deltaTime;
        rotX += my * rotSpeed * Time.deltaTime;

        // rotX 의 값의 -80 ~ 80 도로 제한 (최소값, 최대값)
        rotX = Mathf.Clamp(rotX, -80, 80); // 아래 if문과 같은 내용

        /*
        if(rotX < -80)
        {
            rotX = -80;
        }

        if(rotX > 80)
        {
            rotX = 80;
        }
        */

        // 물체를 회전 각도로 세팅 하자.
        transform.localEulerAngles = new Vector3(-rotX, rotY, 0); // get set으로 되어있고 새로운 Vector3가 필요하다. 맞나? gpt한테 물어보기
        // Vector3는 구조체로 되어 있다.


        MyTransform myTransform = new MyTransform();
        myTransform.position
    }
}

public class MyTransform
{
    public MyVector3 originPosition;

    public MyVector3 position
    {
        get
        {
            return originPosition;
        }

        set
        {
            originPosition = value;
        }
    }
}


public struct MyVector3
{
    public float x;
    public float y;
    public float z;

}
