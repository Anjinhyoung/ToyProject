using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFire : MonoBehaviour
{

    // �Ѿ� ���� ȿ�� ����(Prefab) �������� �� ����  ������Ʈ
    public GameObject bulletImpactFactory;

    // Raycast��  �̿��� �Ѿ� �߻翡 ����Ǵ� Layer ����
    public LayerMask layerMask;

    // ��ź ����(Prefab)
    public GameObject bombFactory;

    // �ִϸ�����
    Animator anim;

    // Aim ������� ����
    bool isAimMode;

    // PlayerMove ������Ʈ
    PlayerMove playerMove;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        // PlayerMove ��������.
        playerMove = GetComponent<PlayerMove>();

        // ���콺 �����
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        // ���콺 ���� ��ư�� ������ 
        if (Input.GetMouseButtonDown(0))
        {
            // �ȴ� �������� !!
            playerMove.SetWalkRun(false);

            FireRay();
        }

        // ���콺 ������ ��ư�� ������
        if (Input.GetMouseButtonDown(1))
        {
            playerMove.SetWalkRun(false);

            // Aim ��� (animatior "Aim" �Ķ���� ���� true)
            isAimMode = true;
            // anim.SetBool("Aim", true);

        }
        // ���콺 ������ ��ư�� ����
        if (Input.GetMouseButtonUp(1))
        {
            // Aim ��� ���� (animatior "Aim" �Ķ���� ���� false)
            isAimMode = false;
            // anim.SetBool("Aim", false);
        }

        // isAimMode�� ���� animator�� Aiming ����  ����
        // isAimMode == true �̸� Aiming ���� 0 ---> 1 ������ ����
        // isAimMode == false �̸� Aiming ���� 1 ---> 0 ������ ����
        anim.SetFloat("Aiming", isAimMode ? 1 : 0, 0.25f * 0.3f, Time.deltaTime);
        
        // �����ӿ� ���� Idle <-> Walk 
        anim.SetFloat("MoveMent", playerMove.isMoving ? 1 : 0, 0.25f * 0.3f, Time.deltaTime);

        // 2��Ű�� ������
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FireBomb();
        }

    }

    void FireBomb()
    {
        // ��ź ����(Prefab) ���� ��ź�� ������
        GameObject bomb = Instantiate(bombFactory);
        // ��ź�� ī�޶� �չ��� 1��ŭ ������ ��ġ ����.
        bomb.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1;
        // bomb.transform.position = Camera.main.transform.forward * 1;   �̰Ŵ� �� �ȴ�. �ָ� ������ �־ ũ��� ������ ������ ���� ���ʹϱ� �� ������  ��ġ�� ���ϰ� ������   ���ؾ� �Ѵ�.
        // ��ź�� �չ����� ī�޶��� �չ������� ����
        bomb.transform.forward = Camera.main.transform.forward;
    }

    void FireRay()
    {
        
        // Fire �� �ִ� �̸� ����
        string fireName = "Fire";

        /*
        // Aim ���� �� �ִ� �̸��� Aim_Fire ��
        if (isAimMode)
        {
            fireName = "Aim_Fire";
        }
        
        */

        // �� ��� �ִϸ��̼� ����
        anim.CrossFade(fireName, 0.01f, 0, 0);
        
        // ī�޶� ��ġ���� ī�޶� �չ������� ���ϴ� Ray�� ������.
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        // Ray�� �߻��ؼ� ��򰡿� �ε����ٸ�
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask))
        {
            // �Ѿ� ���� ȿ���� ��������.
            GameObject bulletImpact = Instantiate(bulletImpactFactory);
            // ������ ȿ���� �ε��� ��ġ�� ����.
            bulletImpact.transform.position = hitInfo.point;

            // ������ ȿ���� �չ����� �ε��� ��ġ�� normal �������� ����
            bulletImpact.transform.forward = hitInfo.normal;

            // �ݻ簢 ������.
            Vector3 outDirection = Vector3.Reflect(ray.direction, hitInfo.normal);

            // ���࿡ �ѿ� ���� ������Ʈ�� Enemy���
            if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                // Enemy ������Ʈ ��������.
                Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                // ������ ������Ʈ�� OnDamaged �Լ��� ����
                enemy.OnDamaged(); 
            }


            // �ε��� ������Ʈ�� �̸��� �ε��� ��ġ�� ����غ���.
            // print(hitInfo.transform.name + "," + hitInfo.transform.position);
            // print(hitInfo.transform.name + "," + hitInfo.point);

            // hitInfo.distance;�̷� �͵� �ִ�.
            // hitInfo.normal ��������(���� ������ ����)
        }
    }
}
