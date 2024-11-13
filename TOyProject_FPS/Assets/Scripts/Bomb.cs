using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �չ������� �ӷ� 5m/s�� ��������

public class Bomb : MonoBehaviour
{
    // �̵� �ӷ�
    public float speed = 10;

    // ���� �ݰ�
    public float exploRange = 3;
    void Start()
    {
        // Rigidbody�� ��������.
        Rigidbody rb = GetComponent<Rigidbody>();
        // ������ Rigidbody�� ���ؼ� �ӵ��� ��������.
        rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� �߽ɿ��� �ݰ� 3���� �ȿ� �ִ� Obstacle ���� �˻�����.
        Collider [] colliders = Physics.OverlapSphere(transform.position, exploRange);

        // �˻��� ��ü�� ��� �ı�����.
        for(int i = 0; i < colliders.Length; i++)
        {
            // ���࿡ �˻��� ��ü�� Layer�� Obstacle�� �ƴϸ� continue �ϰڴ�.

            if (colliders[i].gameObject.layer != LayerMask.NameToLayer("Obstacle")) continue; 
            Destroy(colliders[i].gameObject);
        }

        // ���� �ı�����
        Destroy(gameObject);
    }
}
