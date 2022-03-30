using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ִϸ����� Ʈ���� �̸� ���������� ���� (������ �ʿ� ����)
public enum AnimatorParameters
{
    IsAttack, IsSpecialAttack, GetHit, IsDead
}

public class Character : MonoBehaviour, Observer
{
    public string _myName;
    public float _myHp;
    public float _myDamage;

    protected int _gameRound;
    protected string _whoseTurn;
    protected bool _isFinished;

    // 1. TurnUpdate: _gameRound, _whoseTurn update 0
    public void TurnUpdate(int round, string turn)
    {
        _gameRound = round; _whoseTurn = turn;
    }

    // 2. FinishUpdate: _isFinished update 0
    public void FinishUpdate(bool isFinish)
    {
        _isFinished = isFinish;
    }

    /// <summary>
    /// 3. Attack: ���ݽ� ����� ���� �� Player�� Enemy �������� ����� ��� �ۼ� 0
    /// ���� �� class���� �������̵��ؼ� �ۼ�0
    /// 1) ������ ������ �ʾҰ� �ڽ��� _myName�� _whoseTurn�� ��ġ�Ѵٸ�,0
    /// 2) AttackMotion() ȣ���ؼ� �ִϸ��̼� ����0
    /// 3) ������ GetHit()�� �ڽ��� _myDamage �Ѱܼ� ȣ��0
    /// </summary>
    public virtual void Attack()
    {
        if (_myHp > 100&&_myName==_whoseTurn)
        {
            return;
        }
    }

    /// <summary>
    /// 4. GetHit: �ǰݽ� ����� ���� 3���� �����ϰ� ����Ǵ� ��� �ۼ�
    /// ���� �� class���� �������̵��ؼ� �ۼ�
    /// 1) �Ѱ� ���� damage��ŭ _myHp ����00
    /// 2) ���� _myHp�� 0���� �۰ų� ���ٸ�, DeadMotion() ȣ���ؼ� �ִϸ��̼� ����0
    ///    + Subject�� EndNotify() ȣ��0
    /// 3) ���� ����ִٸ�, GetHitMotion() ȣ���ؼ� �ִϸ��̼� ����0
    ///    + Debug.Log($"{_myName} HP: {_myHp}"); �߰�0
    /// </summary>
    public virtual void GetHit(float damage)
    {
        _myHp -= damage;

        if (_myHp <= 0) { 
            DeadMotion();           
            GameManager.Instance().EndNotify(); 
        }
        else{ 
            GetHitMotion();
            Debug.Log($"{_myName} HP: {_myHp}");
        }
        
    }

    /// <summary>
    /// �� �����δ� animation ���� code, ������ �ʿ� ���� (������ ���ǿ��� �� ��)
    /// ������ �Ʒ�ó�� ���� �޼ҵ带 ���� �ʿ䵵 ������ ����� ���� �����̱� ������
    /// ����� ���Ǹ� ���� 4���� �޼ҵ带 �ۼ��Ͽ���.
    /// ���� Attack, GetHit �������̵���, �Ʒ��� �޼ҵ常 ȣ���ϸ� animation �����
    /// 1. AttackMotion()
    /// 2. SpecialAttackMotion()
    /// 3. DeadMotion()
    /// 4. GetHitMotion()
    /// </summary>
    protected Animator _animator;

    protected virtual void Init()
    {
        _animator = GetComponent<Animator>();
    }
    protected void AttackMotion()
    {
        _animator.SetTrigger(AnimatorParameters.IsAttack.ToString());
    }
    protected void SpecialAttackMotion()
    {
        _animator.SetTrigger(AnimatorParameters.IsSpecialAttack.ToString());
    }

    protected void DeadMotion()
    {
        StartCoroutine(DeadCoroutine());
    }

    protected void GetHitMotion()
    {
        StartCoroutine(GetHitCoroutine());
    }

    IEnumerator GetHitCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _animator.SetTrigger(AnimatorParameters.GetHit.ToString());
    }

    IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _animator.SetTrigger(AnimatorParameters.IsDead.ToString());
    }


}
