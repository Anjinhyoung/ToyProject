using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    // ȸ�� �ӷ�
    public float rotSpeed = 200;
    // ȸ�� �� (������ �����ϱ�)
    float rotY;
    float rotX;

    void Start()
    {
        
    }

    void Update()
    {
        // ���콺 ������ ����  �޾ƿ���
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // ȸ���� ������ ������Ų��.
        rotY += mx * rotSpeed * Time.deltaTime;
        rotX += my * rotSpeed * Time.deltaTime;

        // rotX �� ���� -80 ~ 80 ���� ���� (�ּҰ�, �ִ밪)
        rotX = Mathf.Clamp(rotX, -80, 80); // �Ʒ� if���� ���� ����

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

        // ��ü�� ȸ�� ������ ���� ����.
        transform.localEulerAngles = new Vector3(-rotX, rotY, 0); // get set���� �Ǿ��ְ� ���ο� Vector3�� �ʿ��ϴ�. �³�? gpt���� �����
        // Vector3�� ����ü�� �Ǿ� �ִ�.


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
