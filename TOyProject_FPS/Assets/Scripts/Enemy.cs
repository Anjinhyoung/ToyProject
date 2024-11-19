using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ���� enum(������)
    public enum EEnemyState
    {
        IDLE,
        MOVE,
        ATTACK,
        ATTACK_DELAY,
        DAMAGE,
        DIE
    }

    // ���� ����
    public EEnemyState currState;

    // �÷��̾� ���ӿ�����Ʈ
    GameObject player;

    // ���� ����
    public float traceRange = 8;

    // �̵� �ӷ�
    public float moveSpeed = 3;

    // ���� ����
    public float attackRange = 2;

    // ���� �ð�
    float currTime;

    // ���� Delay �ð�
    public float attackDelayTime = 2;

    // Animator
    Animator anim;



    void Start()
    {
        // �÷��̾ ã��
        player = GameObject.Find("Player");

        // �ڽĿ� �ִ� Animator ã�ƿ���.
        anim = GetComponentInChildren<Animator>();


        // HpSystem�� ��������.
        HpSystem hpSystem = GetComponent<HpSystem>();
        // ������ ������Ʈ����  ondie �Լ��� ���
        hpSystem.onDie = OnDie;
    }

    void Update()
    {
        switch (currState)
        {
            case EEnemyState.IDLE:
                UpdateIdle();
                break;

            case EEnemyState.MOVE:
                UpdateMove();
                break;

            case EEnemyState.ATTACK:
                UpdateAttack();
                break;

            case EEnemyState.ATTACK_DELAY:
                UpdateDelay();
                break;

            case EEnemyState.DAMAGE:
                UpdateDamage();
                break;

            case EEnemyState.DIE:
                //UpdateDie();
                break;
        }
    }


    // ���°� ��Ȱ�� �� �� ���� �����ϴ� ����
    public void ChangeState(EEnemyState state)
    {
        // ���� ���� -> ���� ����
        print(currState + "-------->" + state);

        // ���� ���¸� state�� ����
        currState = state;

        // ���� �ð��� �ʱ�ȭ 
        currTime = 0;


        switch (currState)
        {
            case EEnemyState.IDLE:
                anim.SetTrigger(currState.ToString());
                break;

            case EEnemyState.MOVE:
                // ���� ������ Animation�� ����
                // animator ���� ���� ������ Trigger�� �߻�
                anim.SetTrigger(currState.ToString());
                break;

            case EEnemyState.ATTACK:
                // currTime = attackDelayTime; ������ ��ٷȴٰ� �����ؾ� �ϴµ� �ٷ�  �����ϰ� �ڵ带 �ٲ�
                // anim.SetTrigger(currState.ToString());
                break;

            case EEnemyState.DAMAGE:
                {
                    HpSystem hpSystem = GetComponent<HpSystem>();
                    hpSystem.UpdateHP(-1);
                    anim.SetTrigger(currState.ToString());
                }
                break;

            case EEnemyState.DIE:
                {
                    CapsuleCollider coll = GetComponent<CapsuleCollider>();
                    coll.enabled = false;

                    anim.SetTrigger(currState.ToString());
                }
                break;
        }
    }

    // ��� ������ �� ��� �ؾ� �ϴ� ����
    void UpdateIdle()
    {
        // Player�� ���� �Ÿ��� ������.
        float dist = Vector3.Distance(player.transform.position, transform.position);
        // ���࿡ �� �Ÿ��� �������� ���� ������
        if(dist < traceRange)
        {
            // MOVE ���·� ��ȯ
            ChangeState(EEnemyState.MOVE);
        }

    }

    // �̵������� �� ����ؾ� �ϴ� ����
    void UpdateMove()
    {

        // Player ���� �Ÿ��� ������
        float dist = Vector3.Distance(player.transform.position, transform.position);

        // �� �Ÿ���  ���� �������� ������ 
        if(dist < attackRange)
        {
            // ���¸� ���ݻ��·� ��ȯ
            ChangeState(EEnemyState.ATTACK);
        }

        else if(dist > traceRange)
        {
            ChangeState(EEnemyState.IDLE); 
        }

        // �׷��� ������ 
        else
        {
            // Player�� ���ϴ� ���� ������
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0;
            dir.Normalize(); // ���� ���Ͷ�� �ϸ�, ������ �����ϸ鼭 ������ ũ��(����)�� 1�� �������ϴ�
            // ���� �չ����� dir�� ����.
            transform.forward = dir; // ������ ȸ���ϰ� �� �ؾ� �ϴµ� �ϴ��� �̷���
            // �� �������� �̵�����.
            transform.position += dir * moveSpeed * Time.deltaTime; // ������ ���Ѵ� �ص� �ʱ� ��ġ�� ������ �ʴ´�
        }
    }


    // ���� ������ �� ��� �ؾ� �ϴ� ����

    void UpdateAttack()
    {
        // �÷��̾ ��������.
        print("����! ����!");
        // �÷��̾� HP ������
        HpSystem hpSystem = player.GetComponent<HpSystem>();
        hpSystem.UpdateHP(-2);

        // ����  Animation ����
        anim.SetTrigger(currState.ToString());

        // ���¸� Attack_delay  ���·� ��ȯ
        ChangeState(EEnemyState.ATTACK_DELAY);
    }

    void DecideStateByDist()
    {
        // ���࿡ Player�� �Ÿ��� attackRange ���� ������ 
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < attackRange)
        {
            // ���� �ð�   �ʱ�ȭ
            currTime = 0;
            ChangeState(EEnemyState.ATTACK);
        }

        // �׷��� �ʰ� �������� ���� ������   
        else if (dist < traceRange)
        {
            // �̵� ���·� ��ȯ
            ChangeState(EEnemyState.MOVE);
        }

        // �׷��� �ʰ� �������� ���� ũ��
        else
        {
            // �����·� ��ȯ
            ChangeState(EEnemyState.IDLE);
        }
    }

    private void UpdateDelay()
    {
        // �ð��� �帣�� ����.
        currTime += Time.deltaTime;
        // ���� Delay �ð���ŭ  ��ٷȴٰ�
        if (currTime > attackDelayTime)
        {
            DecideStateByDist();   
        }
    }

    // �ǰ� ��� �ð�
    public float damageDelay = 1;
    void UpdateDamage()
    {
        // �ǰ� �ð���ŭ ��ٷȴٰ� 
        currTime += Time.deltaTime;
        if(currTime > damageDelay)
        {
            DecideStateByDist();
        }

    }

    void OnDie()
    {
        ChangeState(EEnemyState.DIE);
    }

    public void OnDamaged()
    {
        // ���¸� Damage�� ��ȯ
        ChangeState(EEnemyState.DAMAGE);
    }

    // ��������  �ӷ�
    public float downSpeed = 3;
    // �������� ���� ��ٷ��� �ϴ� �ð�

    public float dieDelayTime = 2;

    void UpdateDie()
    {
        // ������ ��������.
        transform.position += Vector3.down * downSpeed * Time.deltaTime;
        // 2�� �ڿ� �ı�����.
        Destroy(gameObject, dieDelayTime);
    }
}
