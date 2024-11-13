using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 나의 앞방향으로 속력 5m/s로 움직이자

public class Bomb : MonoBehaviour
{
    // 이동 속력
    public float speed = 10;

    // 폭파 반경
    public float exploRange = 3;
    void Start()
    {
        // Rigidbody를 가져오자.
        Rigidbody rb = GetComponent<Rigidbody>();
        // 가져온 Rigidbody를 통해서 속도를 설정하자.
        rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // 나의 중심에서 반경 3미터 안에 있는 Obstacle 들을 검색하자.
        Collider [] colliders = Physics.OverlapSphere(transform.position, exploRange);

        // 검색한 물체를 모두 파괴하자.
        for(int i = 0; i < colliders.Length; i++)
        {
            // 만약에 검색된 물체의 Layer가 Obstacle이 아니면 continue 하겠다.

            if (colliders[i].gameObject.layer != LayerMask.NameToLayer("Obstacle")) continue; 
            Destroy(colliders[i].gameObject);
        }

        // 나를 파괴하자
        Destroy(gameObject);
    }
}
