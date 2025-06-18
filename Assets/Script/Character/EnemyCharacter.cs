using System.Collections;
using UnityEngine;

public class EnemyCharacter : TurnBasedCharacter
{
    [SerializeField]
    private EnemyUI _enemyUI;

    protected override void Awake()
    {
        base.Awake();
        _actions.Add(new EnemyAttackAction());
    }

    private void Start()
    {
        _enemyUI.EnemyStatusUI.SetHealthBar(HealthPoint, MaximumHealthPoint);
        OnDamage.AddListener(_enemyUI.EnemyStatusUI.SetHealthBar);
        OnDeath.AddListener(character => _enemyUI.Hide());
        _enemyUI.Show();
    }

    public override void BeginTurn()
    {
        base.BeginTurn();
        StartCoroutine(StartAction());
    }

    public override void EndTurn()
    {
        base.EndTurn();
    }

    private IEnumerator StartAction()
    {
        yield return new WaitForSeconds(3);
        _actions[0].Execute(this);
    }
}
