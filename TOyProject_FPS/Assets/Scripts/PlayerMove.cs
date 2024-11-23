using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

// 스페이스바를 누르면 점프를 하고 싶다.

public class PlayerMove : MonoBehaviour
{
    // 이동 속력
    float moveSpeed;

    // 걷는 속력
    public float walkSpeed = 5.0f;

    // 뛰는 속력
    public float runSpeed = 10.0f;

    // 움직이고 있는지?
    public bool isMoving;

    // 캐릭터 컨트롤러
    public CharacterController cc;

    // 점프파워
    public float jumpPower = 3;
    // 중력
    float gravity = -9.81f;
    // y 방향  속력
    float yVelocity;


    // 최대 점프 횟수
    public int jumpMaxCnt = 2;
    // 현재 점프 횟수
    int jumpCurrCnt;

    // 마우스 우클릭해서 움직일수 있는 상태니?
    bool canMove = false;

    // 움직이여야 하는 방향
    Vector3 movDir;

    // 움직이여야 하는 거리
    float moveDist;

    // Animator 컴포넌트 
    Animator anim;

    void Start()
    {
        // 이동속력을 걷는 속력으로 설정
        moveSpeed = walkSpeed;

        // 캐릭터 컨트롤러  가져오자
        cc = GetComponent<CharacterController>();

        // Animator 가져오자
        anim = GetComponentInChildren<Animator>();

        // HpSystem을 가져오자.
        HpSystem hpSystem = GetComponent<HpSystem>();
        // 가져온 컴포넌트에서  die 함수를 등록
        hpSystem.onDie = Die;
    }
    void Update()
    {
        WalkRun();

         WASD_Move();
         
        /*
        Click_Move();

        if (canMove)
        {
            // 움직일 수 있는 거리를 줄여나가자( 거리  =  속력 * 시간)
            moveDist -= moveSpeed * Time.deltaTime;    

            // 만약에 moveDist가 0보다 같거나 작으면
            if(moveDist <= 0)
            {
                // 움직이지 않게 하자.
                canMove = false;
            }

            // dir 방향으로 움직이자
            cc.Move(movDir * moveSpeed * Time.deltaTime);
        }

        */
    }

    void Click_Move()
    {

        // 마우스 오른쪽 버튼을 누르지 않았다면 함수를 나가자.
        if (Input.GetMouseButtonDown(1) == false) return;

        // 화면 좌표에서 Ray르  만들자.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //  Raycast를 이용해서 부딪힌 정보를 얻어오자.
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, float.MaxValue, 1 << LayerMask.NameToLayer("Ground")))
        {
            // 움직일 수 있는 상태로 바꾸자
            canMove = true;

            // Player 이동해야하 는 방향을 구하자(검출된 위치에서 - 플레이어의 위치)
            movDir = hitInfo.point - transform.position; // 이렇게 벡터에서 뺄 수 있구나 상기시키기 알고 있었음


            // 움직이여야 하는 거리를 셋팅
            moveDist = movDir.magnitude;

            // dir 크기를 Normalize 하자.
            movDir.Normalize();

            // print("클릭된 물체 이름:" + hitInfo.transform.name); // 역으로 하는 거 
            // print("클릭 된 곳에 3D 좌표" + hitInfo.point);
        }
    }

    void WASD_Move()
    {
        // W, A, S, D 키로 앞, 뒤, 왼, 오른 으로 움직이게 하자.
        // 1. 사용자의 입력을 받자 (W, A, S, D 키 입력)
        float h = Input.GetAxis("Horizontal"); // A: -1 , D: 1, 누르지 않으면 0
        float v = Input.GetAxis("Vertical");    // S: -1, W: 1, 누르지 않으면  0

        // 2. 방향을 정하자.
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;
        // 움직이고 있는지 판별
        isMoving = dir.sqrMagnitude > 0;

        // dir의 크기를 1로 만들자 (벡터의 정규화)
        dir.Normalize();

        // 만약에 땅에 있다면 yVelocity를 0으로 초기화 (이거 위치가 중요함 만약에 점프하고 난 뒤에 위치해 있으면 점프 명령을
        // 입력해도 캐릭터가 땅에 있는 동안 yVelocity가 0으로 즉시 초기화되기 때문에 점프가 일어나지 않거나 중력이 즉각적으로 다시 적용됩니다.)
        if (cc.isGrounded)
        {
            yVelocity = 0;
            jumpCurrCnt = 0;
        }

        // 만약에 스페이스바를 누르면
        if (Input.GetButtonDown("Jump"))
        {
            // 만약에 현재 점프 횟수가 최대 점프 횟수 보다 작으면 
            if (jumpCurrCnt < jumpMaxCnt)
            {
                // yVelocity에 jumpPower를 셋팅
                yVelocity = jumpPower;
                // 현재 점프 횟수를 증가 시키자.
                jumpCurrCnt++;
            }
        }
        // yVelocity를 중력값을 이용해서 감소시킨다.
        // v = v0 + at;   v는 속력  a는 가속도
        yVelocity += gravity * Time.deltaTime;

        // dir.y 값에 yVelocity를 셋팅
        dir.y = yVelocity;

        // 3. 그 방향으로 움직이자. (P = P0 + vt)
        // transform.position += dir * moveSpeed * Time.deltaTime;
        cc.Move(dir * moveSpeed * Time.deltaTime);  // 이렇게 해야 충돌처리가 난다.
    }

    void WalkRun()
    {
        // 왼쪽 Shift 키를 누르면 
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetWalkRun(true);
        }
        // 왼쪽 Shift 키를 떼면 
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SetWalkRun(false);
        }
    }

    public void SetWalkRun(bool isRun)
    {
        // isRun에 따라서 MoveSpeed 변경
        moveSpeed = isRun ? runSpeed : walkSpeed;
        // isRun에 따라서 애니메이션도 변경
        anim.SetBool("Run", isRun);
    }

    public GameObject model;
     public void Die()
    {
        // Model 게임 오브젝트를 비활성화
        model.SetActive(false);
    }
}
