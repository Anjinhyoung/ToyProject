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

    // 회전 허용
    public bool useRotX;
    public bool useRotY;

    void Start()
    {
        
    }

    void Update()
    {
        // 마우스 움직임 값을  받아오자
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        // 회전한 각도를 누적시킨다.
        if(useRotY) rotY += mx * rotSpeed * Time.deltaTime;
        if(useRotX) rotX += my * rotSpeed * Time.deltaTime;

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
        
        #region 레퍼런스 타입과 value 타입의 경우 반환되었을 때 멤버변수 접근 허용/ 비허용
        MyTransform myTransform = new MyTransform();
        myTransform.position = new MyVector3(10,10,10); //  set을 호출
        // print(myTransform.position);
        // myTransform.position.x = 20; 이거는 안 된다. 이유는 값 타입 이기 때문에 읽기 전용 속성에서는 필드를 수정할 수 없다.
        // 바꿀려면 새로 할당해야 한다.

        // 당시 한 번 상기: new 키워드 없이 구조체나 클래스를 직접 생성 할려고 하면 안 된다.
        myTransform.SetVector(new MyVector3(10, 10, 10));
        // print(myTransform.GetVector());

        // print(myTransform.GetVector().x); // struct일 때는 안 되지만 class일 때는 가능하다.

        #endregion
        

        
    }
}








#region 레퍼런스 타입과 value 타입의 경우 반환되었을 때 멤버변수 접근 허용/ 비허용
public class MyTransform
{
    public MyVector3 originPosition;

    public MyVector3 position
    {
        get // 값을 반환
        {
            return originPosition;
        }

        set // 값을 설정
        {
            // value는 C#에서 자동적으로 제공되는 키워드 속성에서 값을 설정할 때 value라는 이름을 자동으로 사용합니다.
            // 사용자가 속성에 값을 할당할 때 그 할당된 값이 들어가게 된다.
            // C#의 속성(set) 접근자에서는 value를 사용해야 외부에서 전달된 값을 처리할 수 있다.
            originPosition = value;
        }
    }

    public MyVector3 GetVector()
    {
        return originPosition;
    }

    // 값을 새로 할당하는 메서드
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
    
    // 구조체 생성자
    public MyVector3(int _x, int _y, int _z)
    {
        // 초기화
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

    // 구조체 생성자
    public MyVector3(int _x, int _y, int _z)
    {
        // 초기화
        x = _x;
        y = _y;
        z = _z;
    }
}
#endregion



