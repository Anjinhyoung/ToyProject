using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAniEvent : MonoBehaviour
{
    // �θ��� Enemy ������Ʈ ���� ����
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // ���� �ؾ��ϴ� Ÿ�ֿ̹� ȣ��Ǵ� �Լ�
    public void OnAttack()
    {
        enemy.RealAttack();
    }
}
