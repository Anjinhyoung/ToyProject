using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// delegate: 함수를 담을 수 있는 자료형

public class HpSystem : MonoBehaviour
{
    // 최대 HP (public할 때 조심하자)
    public float maxHP = 3;
    // 현재 HP
    float currHP;

    // HPBar UI
    public Image hpBar;


    // Hp가 0이 되었을 때 호출되는 함수를 담을 변수
    public Action onDie;

    void Start()
    {
        // 현재 HP를 최대 HP로 설정
        currHP = maxHP;

    }

    void Update()
    {
        
    }

    public void UpdateHP(float value)
    {
        // 현재 HP를 value 만큼 더하자
        currHP += value;

        // HPBar UI 갱신 (0 ~ 1)
        hpBar.fillAmount = currHP / maxHP;

        // 만약에 현재 HP가 0보다 작거나 같으면
        if (currHP <= 0)
        {
            // onDie에 있는 함수를 실행하자.
            if(onDie != null)
            {
                onDie();
            }


            /*

            // Enemy 컴포넌트 가져오자.
            Enemy enemy = GetComponent<Enemy>(); // 인스펙터에 Enemy script랑 같이 붙어 있어서 굳이 오브젝트에서 안 갖고 와도 괜찮다
            if(enemy != null)
            {
                // 가져온 컴포넌트의 ChangeState 함수를 호출 (Die 상태로 전환)
                enemy.ChangeState(Enemy.EEnemyState.DIE);
            }

            // PlayerMove 컴포넌트 가져오자.
            PlayerMove  player = GetComponent<PlayerMove>();
            if(player != null)
            {
                // Die 함수 실행시키자
                player.Die();
            }

            // 파과하자.
            // Destroy(gameObject);

            

            */
        }
    }
}
