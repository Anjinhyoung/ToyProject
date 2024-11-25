using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    // Enemy ����(Prefab)
    public GameObject enemyFactory;

    // ���� �ð�
    float currTime;
    // ���� �ð�
    float creatTime = 1;

    // ����Ǵ� �ڷ�ƾ ���� ����
    Coroutine coroutine;
    void Start()
    {
        coroutine = StartCoroutine(Delay());
    }

    void Update()
    {
        // �ڷ�ƾ ���߱�
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // ��ü �ڷ�ƾ ���߱�
            // StopAllCoroutines();

            // Ư�� �ڷ�ƾ ���߱�

            StopCoroutine(coroutine);
            coroutine = null;
        }
        
        /*
        // �ð��� ���� ��Ű��
        currTime += Time.deltaTime;
        // ���࿡ ���� �ð��� ���� �ð����� Ŀ����
        if(currTime > creatTime)
        {
            // Enemy�� ������.
            CreateEnemy();

            // �ð� �ʱ�ȭ
            currTime = 0;
        }
        */
    }

    void CreateEnemy()
    {
        // ���� ��ġ
        Vector3 pos = new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f));
        Instantiate(enemyFactory, pos, transform.rotation);

    }
    
    // �ڷ�ƾ �Լ�
    IEnumerator Delay()
    
    {




        while (true)
        {
            print("1�� ��ٸ��� ��");
            yield return new WaitForSeconds(1);
            print("�ڷ�ƾ ��");
            CreateEnemy();
        }
    }
}
