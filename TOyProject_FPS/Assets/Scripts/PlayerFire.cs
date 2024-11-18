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
    


    void Start()
    {
    }

    void Update()
    {




        // 1��Ű�� ������
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FireRay();
        }

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
