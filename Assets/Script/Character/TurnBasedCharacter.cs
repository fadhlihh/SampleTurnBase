using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnBasedCharacter : Character, IDamagable
{
    [SerializeField]
    private CharacterData _data;
    [SerializeField]
    private Transform _attackerPosition;

    protected List<TurnBasedAction> _actions = new List<TurnBasedAction>();

    public UnityEvent OnBeginTurn;
    public UnityEvent OnEndTurn;
    public UnityEvent<float, float> OnCharacterDamage;
    public UnityEvent<TurnBasedCharacter> OnCharacterDeath;
    public UnityEvent OnAttack;

    private TurnBasedCharacter _target;
    private Vector3 _originPosition;

    public List<TurnBasedAction> Actions { get => _actions; }
    public UnityEvent<float, float> OnDamage => OnCharacterDamage;
    public UnityEvent<TurnBasedCharacter> OnDeath => OnCharacterDeath;
    public int MaximumHealthPoint { get; set; }
    public int HealthPoint { get; set; }
    public int MaximumSkillPoint { get; set; }
    public int SkillPoint { get; set; }
    public int DamagePoint { get; set; }
    public int DefensePoint { get; set; }
    public int Speed { get; set; }
    public bool IsDefending { get; set; }
    public bool IsDead { get; protected set; }
    public CharacterData Data { get => _data; }
    public Vector3 AttackerPosition { get => (_attackerPosition != null) ? _attackerPosition.position : Vector3.zero; }

    protected virtual void Awake()
    {
        InitializeData();
    }

    public virtual void BeginTurn()
    {
        Debug.Log($"{Data.Name} Begin Turn");
        OnBeginTurn?.Invoke();
    }

    public virtual void EndTurn()
    {
        Debug.Log($"{Data.Name} End Turn");
        OnEndTurn?.Invoke();
        TurnBasedManager.Instance.NextTurn();
    }

    public void Damage(int value)
    {
        int damageModifier = 0;
        damageModifier += IsDefending ? -DefensePoint : 0;
        HealthPoint -= value + damageModifier;
        OnDamage?.Invoke(HealthPoint, MaximumHealthPoint);
        IsDefending = false;
        if (HealthPoint <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        IsDead = true;
        OnDeath?.Invoke(this);
    }

    public void HandleHitTarget()
    {
        _target.Damage(DamagePoint);
    }

    public void PerformAttack(TurnBasedCharacter target)
    {
        _target = target;
        transform.position = target.AttackerPosition;
        IsDefending = false;
        OnAttack?.Invoke();
    }

    public void HandleEndAction()
    {
        _target = null;
        transform.position = _originPosition;
        EndTurn();
    }

    public void SetSpeed(int value)
    {
        Speed = value;
    }

    protected void InitializeData()
    {
        HealthPoint = Data.MaximumHealthPoint;
        MaximumHealthPoint = Data.MaximumHealthPoint;
        SkillPoint = Data.MaximumSkillPoint;
        MaximumSkillPoint = Data.MaximumSkillPoint;
        DamagePoint = Data.DamagePoint;
        DefensePoint = Data.DefensePoint;
        Speed = Data.Speed;
        _originPosition = transform.position;
    }
}
