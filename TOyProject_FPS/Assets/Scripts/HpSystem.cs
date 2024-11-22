using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// delegate: �Լ��� ���� �� �ִ� �ڷ���

public class HpSystem : MonoBehaviour
{
    // �ִ� HP (public�� �� ��������)
    public float maxHP = 3;
    // ���� HP
    float currHP;

    // HPBar UI
    public Image hpBar;


    // Hp�� 0�� �Ǿ��� �� ȣ��Ǵ� �Լ��� ���� ����
    public Action onDie;

    void Start()
    {
        // ���� HP�� �ִ� HP�� ����
        currHP = maxHP;

    }

    void Update()
    {
        
    }

    public void UpdateHP(float value)
    {
        // ���� HP�� value ��ŭ ������
        currHP += value;

        // HPBar UI ���� (0 ~ 1)
        hpBar.fillAmount = currHP / maxHP;

        // ���࿡ ���� HP�� 0���� �۰ų� ������
        if (currHP <= 0)
        {
            // onDie�� �ִ� �Լ��� ��������.
            if(onDie != null)
            {
                onDie();
            }


            /*

            // Enemy ������Ʈ ��������.
            Enemy enemy = GetComponent<Enemy>(); // �ν����Ϳ� Enemy script�� ���� �پ� �־ ���� ������Ʈ���� �� ���� �͵� ������
            if(enemy != null)
            {
                // ������ ������Ʈ�� ChangeState �Լ��� ȣ�� (Die ���·� ��ȯ)
                enemy.ChangeState(Enemy.EEnemyState.DIE);
            }

            // PlayerMove ������Ʈ ��������.
            PlayerMove  player = GetComponent<PlayerMove>();
            if(player != null)
            {
                // Die �Լ� �����Ű��
                player.Die();
            }

            // �İ�����.
            // Destroy(gameObject);

            

            */
        }
    }
}
