using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFire : MonoBehaviour
{

    // �Ѿ� ���� ȿ�� ����(Prefab) �������� �� ����  ������Ʈ
    public GameObject bulletImpactFactory;

    // Raycast��  �̿��� �Ѿ� �߻翡 ����Ǵ� Layer ����
    public LayerMask layerMask;
    void Start()
    {
        
    }

    void Update()
    {
        // 1��Ű�� ������
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // ī�޶� ��ġ���� ī�޶� �չ������� ���ϴ� Ray�� ������.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // Ray�� �߻��ؼ� ��򰡿� �ε����ٸ�
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask))
            {
                // �Ѿ� ���� ȿ���� ��������.
                GameObject bulletImpact =  Instantiate(bulletImpactFactory);
                // ������ ȿ���� �ε��� ��ġ�� ����.
                bulletImpact.transform.position = hitInfo.point;

                // ������ ȿ���� �չ����� �ε��� ��ġ�� normal �������� ����
                bulletImpact.transform.forward = hitInfo.normal;

                // �ݻ簢 ������.
                Vector3 outDirection = Vector3.Reflect(ray.direction, hitInfo.normal);

                // �ε��� ������Ʈ�� �̸��� �ε��� ��ġ�� ����غ���.
                // print(hitInfo.transform.name + "," + hitInfo.transform.position);
                // print(hitInfo.transform.name + "," + hitInfo.point);

                // hitInfo.distance;�̷� �͵� �ִ�.
                // hitInfo.normal ��������(���� ������ ����)
            }

        }

    }
}
