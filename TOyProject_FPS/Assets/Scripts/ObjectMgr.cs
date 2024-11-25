using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    // Enemy 공장(Prefab)
    public GameObject enemyFactory;

    // 현재 시간
    float currTime;
    // 생성 시간
    float creatTime = 1;

    // 실행되는 코루틴 담을 변수
    Coroutine coroutine;
    void Start()
    {
        coroutine = StartCoroutine(Delay());
    }

    void Update()
    {
        // 코루틴 멈추기
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // 전체 코루틴 멈추기
            // StopAllCoroutines();

            // 특정 코루틴 멈추기

            StopCoroutine(coroutine);
            coroutine = null;
        }
        
        /*
        // 시간을 증가 시키자
        currTime += Time.deltaTime;
        // 만약에 현재 시간이 생성 시간보다 커지면
        if(currTime > creatTime)
        {
            // Enemy를 만들자.
            CreateEnemy();

            // 시간 초기화
            currTime = 0;
        }
        */
    }

    void CreateEnemy()
    {
        // 랜덤 위치
        Vector3 pos = new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f));
        Instantiate(enemyFactory, pos, transform.rotation);

    }
    
    // 코루틴 함수
    IEnumerator Delay()
    
    {




        while (true)
        {
            print("1초 기다리는 중");
            yield return new WaitForSeconds(1);
            print("코루틴 끝");
            CreateEnemy();
        }
    }
}
