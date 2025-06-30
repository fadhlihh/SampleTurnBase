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
        _actions.Add(new EnemySkillAction(Skills));
        _actions.Add(new EnemyItemAction(Items));
    }

    private void Start()
    {
        _enemyUI.EnemyStatusUI.SetHealthBar(HealthPoint, MaximumHealthPoint);
        OnDamage.AddListener(_enemyUI.EnemyStatusUI.SetHealthBar);
        OnDeath.AddListener(character => _enemyUI.Hide());
        OnHeal.AddListener(_enemyUI.EnemyStatusUI.SetHealthBar);
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
        int roll = Random.Range(1, 7); // 1 sampai 6
        TurnBasedAction chosenAction;

        if (roll <= 3)
        {
            // 1,2,3 = Attack
            chosenAction = _actions.Find(a => a.Type == EActionCategory.Attack);
        }
        else if (roll <= 5)
        {
            // 4,5 = Skill
            chosenAction = _actions.Find(a => a.Type == EActionCategory.Skill);
        }
        else
        {
            // 6 = Item
            chosenAction = _actions.Find(a => a.Type == EActionCategory.Item);
        }

        if (chosenAction != null)
        {
            chosenAction.Execute(this);
        }
        else
        {
            Debug.LogWarning("[EnemyCharacter] No valid action found for roll: " + roll);
            EndTurn(); // fail safe
        }
    }
}
