using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����̽��ٸ� ������ ������ �ϰ� �ʹ�.

public class PlayerMove : MonoBehaviour
{
    float moveSpeed = 5.0f;

    // ĳ���� ��Ʈ�ѷ�
    public CharacterController cc;

    // �����Ŀ�
    public float jumpPower = 3;
    // �߷�
    float gravity = -9.81f;
    // y ����  �ӷ�
    float yVelocity;


    // �ִ� ���� Ƚ��
    public int jumpMaxCnt = 2;
    // ���� ���� Ƚ��
    int jumpCurrCnt;

    void Start()
    {
        // ĳ���� ��Ʈ�ѷ�  ��������
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        // W, A, S, D Ű�� ��, ��, ��, ���� ���� �����̰� ����.
        // 1. ������� �Է��� ���� (W, A, S, D Ű �Է�)
        float h = Input.GetAxis("Horizontal"); // A: -1 , D: 1, ������ ������ 0
        float v = Input.GetAxis("Vertical");    // S: -1, W: 1, ������ ������  0

        // 2. ������ ������.
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;

        // dir�� ũ�⸦ 1�� ������ (������ ����ȭ)
        dir.Normalize();



        // ���࿡ ���� �ִٸ� yVelocity�� 0���� �ʱ�ȭ (�̰� ��ġ�� �߿��� ���࿡ �����ϰ� �� �ڿ� ��ġ�� ������ ���� �����
        // �Է��ص� ĳ���Ͱ� ���� �ִ� ���� yVelocity�� 0���� ��� �ʱ�ȭ�Ǳ� ������ ������ �Ͼ�� �ʰų� �߷��� �ﰢ������ �ٽ� ����˴ϴ�.)
        if (cc.isGrounded)
        {
            yVelocity = 0;
            jumpCurrCnt = 0;
        }

        // ���࿡ �����̽��ٸ� ������
        if (Input.GetButtonDown("Jump"))
        {
            // ���࿡ ���� ���� Ƚ���� �ִ� ���� Ƚ�� ���� ������ 
            if(jumpCurrCnt < jumpMaxCnt)
            { 
                // yVelocity�� jumpPower�� ����
                yVelocity = jumpPower;
                // ���� ���� Ƚ���� ���� ��Ű��.
                jumpCurrCnt++;
            }
        }
        // yVelocity�� �߷°��� �̿��ؼ� ���ҽ�Ų��.
        // v = v0 + at;   v�� �ӷ�  a�� ���ӵ�
        yVelocity += gravity * Time.deltaTime;

        // dir.y ���� yVelocity�� ����
        dir.y = yVelocity;

        // 3. �� �������� ��������. (P = P0 + vt)
        // transform.position += dir * moveSpeed * Time.deltaTime;
        cc.Move(dir * moveSpeed * Time.deltaTime);  // �̷��� �ؾ� �浹ó���� ����.
    }
}
