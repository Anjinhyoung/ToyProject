using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 상태 enum(열거형)
    public enum EEnemyState
    {
        IDLE,
        MOVE,
        ATTACK,
        DAMAGE,
        DIE
    }

    // 현재 상태
    public EEnemyState currState;

    // 플레이어 게임오브젝트
    GameObject player;

    // 인지 범위
    public float traceRange = 8;

    // 이동 속력
    public float moveSpeed = 3;

    // 공격 범위
    public float attackRange = 2;

    // 현재 시간
    float currTime;

    // 공격 Delay 시간
    public float attackDelayTime = 2;

    void Start()
    {
        // 플레이어를 찾자
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

    // 상태가 전활될 때 한 번만 실행하는 동작
    void ChangeState(EEnemyState state)
    {
        // 이전 상태 -> 현재 상태
        print(currState + "-------->" + state);

        // 현재 상태를 state로 설정
        currState = state;

        // 현재 시간을 초기화
        currTime = 0;

        switch (currState)
        {
            case EEnemyState.ATTACK:
                currTime = attackDelayTime;
                break;
        }
    }

    // 대기 상태일 때 계속 해야 하는 동작
    void UpdateIdle()
    {
        // Player와 나의 거리를 구하자.
        float dist = Vector3.Distance(player.transform.position, transform.position);
        // 만약에 그 거리가 인지범위 보다 작으면
        if(dist < traceRange)
        {
            // MOVE 상태로 전환
            ChangeState(EEnemyState.MOVE);

        }

    }

    // 이동상태일 때 계속해야 하는 동작
    void UpdateMove()
    {

        // Player 와의 거리를 구하자
        float dist = Vector3.Distance(player.transform.position, transform.position);

        // 그 거리가  공격 범위보다 작으면 
        if(dist < attackRange)
        {
            // 상태를 공격상태로 전환
            ChangeState(EEnemyState.ATTACK);
        }

        // 그렇지 않으면 
        else
        {
            // Player를 향하는 방향 구하자
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0;
            dir.Normalize(); // 단위 벡터라고 하며, 방향은 유지하면서 벡터의 크기(길이)는 1로 맞춰집니다
            // 나의 앞방향을 dir로 하자.
            transform.forward = dir; // 원래는 회전하고 뭐 해야 하는데 일단은 이렇게
            // 그 방향으로 이동하자.
            transform.position += dir * moveSpeed * Time.deltaTime; // 방향을 구한다 해도 초기 위치는 변하지 않는다
        }
    }


    // 공격 상태일 때 계속 해야 하는 동작

    void UpdateAttack()
    {
        // 시간을 흐르게 하자.
        currTime += Time.deltaTime;
        // 공격 Delay 시간만큼  기다렸다가
        if(currTime > attackDelayTime)
        {
            // 만약에 Player와 거리가 attackRange 보다 작으면 
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if(dist < attackRange)
            {
                // 플레이어를 공격하자.
                print("공격! 공격!");
                // 현재 시간   초기화
                currTime = 0;

            }

            // 그렇지 않고 인지범위 보다 작으면   
            else if(dist < traceRange)
            {
                // 이동 상태로 전환
                ChangeState(EEnemyState.MOVE); 
            }

            // 그렇지 않고 인지범위 보다 크면
            else
            {
                // 대기상태로 전환
                ChangeState(EEnemyState.IDLE);
            }
        }
    }
}
