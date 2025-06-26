using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TurnBasedCharacter : Character, IDamagable
{
    [SerializeField]
    private CharacterData _data;
    [SerializeField]
    private Transform _attackerPosition;
    [SerializeField]
    private List<SkillData> _skills = new List<SkillData>();
    [SerializeField]
    private List<ItemData> _items = new List<ItemData>();

    protected List<TurnBasedAction> _actions = new List<TurnBasedAction>();

    public UnityEvent OnBeginTurn;
    public UnityEvent OnEndTurn;
    public UnityEvent<float, float> OnCharacterDamage;
    public UnityEvent<TurnBasedCharacter> OnCharacterDeath;
    public UnityEvent OnAttack;
    public UnityEvent<float, float> OnPerformedSkill;
    public UnityEvent<float, float> OnHeal;
    public UnityEvent<float, float> OnRestoreSkillPoint;

    private TurnBasedCharacter _target;
    private Vector3 _originPosition;
    private List<StatModifier> _modifiers = new List<StatModifier>();

    public List<TurnBasedAction> Actions { get => _actions; }
    public UnityEvent<float, float> OnDamage => OnCharacterDamage;
    public UnityEvent<TurnBasedCharacter> OnDeath => OnCharacterDeath;
    public int MaximumHealthPoint { get; set; }
    public int HealthPoint { get; set; }
    public int MaximumSkillPoint { get; set; }
    public int SkillPoint { get; set; }
    public int BaseDamagePoint { get; set; }
    public int BaseDefensePoint { get; set; }
    public int BaseSpeed { get; set; }
    public int DamagePoint => CalculateModifiedStat(EStatType.Damage);
    public int DefensePoint => CalculateModifiedStat(EStatType.Defense);
    public int Speed => CalculateModifiedStat(EStatType.Speed);
    public bool IsDefending { get; set; }
    public bool IsDead { get; protected set; }
    public CharacterData Data { get => _data; }
    public Vector3 AttackerPosition { get => (_attackerPosition != null) ? _attackerPosition.position : Vector3.zero; }
    public List<SkillData> Skills { get => _skills; }
    public List<ItemData> Items { get => _items; }

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
        ReduceBuffDuration();
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

    public virtual void PerformSkill(int skillPointCost)
    {
        OnPerformedSkill?.Invoke(SkillPoint, MaximumSkillPoint);
        EndTurn();
    }

    public void HandleEndAction()
    {
        _target = null;
        transform.position = _originPosition;
        EndTurn();
    }

    public void ApplyBuff(EStatType type, int value, int duration)
    {
        _modifiers.Add(new StatModifier(type, value, duration));
    }

    public int CalculateModifiedStat(EStatType type)
    {
        int baseValue = type switch
        {
            EStatType.Speed => BaseSpeed,
            EStatType.Damage => BaseDamagePoint,
            EStatType.Defense => BaseDamagePoint
        };

        int modifiers = _modifiers.Where(item => item.Type == type).Sum(item => item.Value);
        return baseValue + modifiers;
    }

    public void ReduceBuffDuration()
    {
        for (int i = _modifiers.Count - 1; i >= 0; i--)
        {
            _modifiers[i].Duration--;
            if (_modifiers[i].Duration == 0)
            {
                _modifiers.RemoveAt(i);
            }
        }
    }

    public void Heal(int value)
    {
        HealthPoint = Math.Clamp(HealthPoint + value, 0, MaximumHealthPoint);
        Debug.Log($"Heal: {HealthPoint}");
        OnHeal?.Invoke(HealthPoint, MaximumHealthPoint);
    }

    public void RestoreSkillPoint(int value)
    {
        SkillPoint = Math.Clamp(SkillPoint + value, 0, MaximumSkillPoint);
        Debug.Log($"Restore: {SkillPoint}");
        OnRestoreSkillPoint?.Invoke(SkillPoint, MaximumSkillPoint);
    }

    protected void InitializeData()
    {
        HealthPoint = Data.MaximumHealthPoint;
        MaximumHealthPoint = Data.MaximumHealthPoint;
        SkillPoint = Data.MaximumSkillPoint;
        MaximumSkillPoint = Data.MaximumSkillPoint;
        BaseDamagePoint = Data.DamagePoint;
        BaseDefensePoint = Data.DefensePoint;
        BaseSpeed = Data.Speed;
        _originPosition = transform.position;
    }
}
