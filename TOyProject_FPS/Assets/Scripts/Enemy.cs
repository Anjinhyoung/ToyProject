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

    void Start()
    {
        // �÷��̾ ã��
        player = GameObject.Find("Player");
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

            case EEnemyState.DAMAGE:
                break;

            case EEnemyState.DIE:
                break;
        }
    }

    // ���°� ��Ȱ�� �� �� ���� �����ϴ� ����
    void ChangeState(EEnemyState state)
    {
        // ���� ���� -> ���� ����
        print(currState + "-------->" + state);

        // ���� ���¸� state�� ����
        currState = state;

        // ���� �ð��� �ʱ�ȭ
        currTime = 0;

        switch (currState)
        {
            case EEnemyState.ATTACK:
                currTime = attackDelayTime;
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
        // �ð��� �帣�� ����.
        currTime += Time.deltaTime;
        // ���� Delay �ð���ŭ  ��ٷȴٰ�
        if(currTime > attackDelayTime)
        {
            // ���࿡ Player�� �Ÿ��� attackRange ���� ������ 
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if(dist < attackRange)
            {
                // �÷��̾ ��������.
                print("����! ����!");
                // ���� �ð�   �ʱ�ȭ
                currTime = 0;

            }

            // �׷��� �ʰ� �������� ���� ������   
            else if(dist < traceRange)
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
    }
}
