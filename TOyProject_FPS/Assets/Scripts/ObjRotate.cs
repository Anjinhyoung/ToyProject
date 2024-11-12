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

    // ȸ�� ���
    public bool useRotX;
    public bool useRotY;

    void Start()
    {
        
    }

    void Update()
    {
        // ���콺 ������ ����  �޾ƿ���
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // ȸ���� ������ ������Ų��.
        if(useRotY) rotY += mx * rotSpeed * Time.deltaTime;
        if(useRotX) rotX += my * rotSpeed * Time.deltaTime;

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
        
        #region ���۷��� Ÿ�԰� value Ÿ���� ��� ��ȯ�Ǿ��� �� ������� ���� ���/ �����
        MyTransform myTransform = new MyTransform();
        myTransform.position = new MyVector3(10,10,10); //  set�� ȣ��
        // print(myTransform.position);
        // myTransform.position.x = 20; �̰Ŵ� �� �ȴ�. ������ �� Ÿ�� �̱� ������ �б� ���� �Ӽ������� �ʵ带 ������ �� ����.
        // �ٲܷ��� ���� �Ҵ��ؾ� �Ѵ�.

        // ��� �� �� ���: new Ű���� ���� ����ü�� Ŭ������ ���� ���� �ҷ��� �ϸ� �� �ȴ�.
        myTransform.SetVector(new MyVector3(10, 10, 10));
        // print(myTransform.GetVector());

        // print(myTransform.GetVector().x); // struct�� ���� �� ������ class�� ���� �����ϴ�.

        #endregion
        

        
    }
}








#region ���۷��� Ÿ�԰� value Ÿ���� ��� ��ȯ�Ǿ��� �� ������� ���� ���/ �����
public class MyTransform
{
    public MyVector3 originPosition;

    public MyVector3 position
    {
        get // ���� ��ȯ
        {
            return originPosition;
        }

        set // ���� ����
        {
            // value�� C#���� �ڵ������� �����Ǵ� Ű���� �Ӽ����� ���� ������ �� value��� �̸��� �ڵ����� ����մϴ�.
            // ����ڰ� �Ӽ��� ���� �Ҵ��� �� �� �Ҵ�� ���� ���� �ȴ�.
            // C#�� �Ӽ�(set) �����ڿ����� value�� ����ؾ� �ܺο��� ���޵� ���� ó���� �� �ִ�.
            originPosition = value;
        }
    }

    public MyVector3 GetVector()
    {
        return originPosition;
    }

    // ���� ���� �Ҵ��ϴ� �޼���
    public void SetVector(MyVector3 value)
    {
        originPosition = value;
    }
}



/*
public struct MyVector3
{
    public float x;
    public float y;
    public float z;
    
    // ����ü ������
    public MyVector3(int _x, int _y, int _z)
    {
        // �ʱ�ȭ
        x = _x;
        y = _y;
        z = _z;
    }
}
*/

public class MyVector3
{
    public float x;
    public float y;
    public float z;

    // ����ü ������
    public MyVector3(int _x, int _y, int _z)
    {
        // �ʱ�ȭ
        x = _x;
        y = _y;
        z = _z;
    }
}
#endregion



