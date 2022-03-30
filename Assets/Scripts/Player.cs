using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character//, Subject //���� ���� ��?
{
    private Enemy _enemy;
    private float _randomAttack;

    /// <summary>
    /// 1. Init: �ʱ�ȭ ���
    /// 1) Subject�� Observer�� ��� 
    /// 2) _myName, _myHp, _myDamage �ʱ�ȭ 0
    /// 3) _myName�� ������ "Player"�� �� �� 0
    /// 4) _myHp, _myDamage�� 100, 20���� ���� �ʱ�ȭ (���� ����) 0
    /// </summary>
    protected override void Init()
    {
        base.Init();
        
        GameManager.Instance().AddCharacter(this);
        this._myName = "Player"; this._myHp = 100; this._myDamage = 20;
    }


    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 1) _enemy�� �Ҵ��� �ȵƴٸ�,
    /// 2) GameObject.FindWithTag �̿��ؼ� _enemy �Ҵ�
    /// </summary>
    /// done(maybe)
    private void Start()
    {
        if (_enemy == null)
        {
            _enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        }
    }

    /// <summary>
    /// Attack:
    /// 1) Player�� 30%�� Ȯ���� ���ݷ��� �� ���� ������ ���� ��
    /// 2) _randomAttack = Random.Range(0,10); ���� ���� ���� ����
    ///   -> 0~9 ������ ���� �� �ϳ��� �������� �Ҵ����.
    /// 3) _randomAttack �̿��ؼ� 30% Ȯ���� ���� ���ݷº��� 10 ���� ���� ����
    /// 4) �̶��� AttackMotion() ���� SpecialAttackMotion() ȣ���� ��
    ///    + Debug.Log($"{_myName} Special Attack!"); �߰�
    /// 5) 70% Ȯ���� �ϴ� �Ϲ� ������ Character�� ���ִ� �ּ��� ����
    /// </summary>

    public override void Attack()
    {
        if (_myHp > 0 && _myName == _whoseTurn)
        {
            _randomAttack = Random.Range(0, 10);
            if (_randomAttack < 7)
            {
                this.AttackMotion();
                Debug.Log($"{_myName} Attack!");
                _enemy.GetHit(_myDamage);
            }
            else
            {
                this.SpecialAttackMotion();
                Debug.Log($"{_myName} Special Attack!");
                _enemy.GetHit(_myDamage + 10);
            }
        }



    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
    }


    //�ӽ�
    //public string GetName()
    //{
    //    return _myName;
    //}
}
