using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : TurnBasedCharacter
{
    protected override void Awake()
    {
        base.Awake();
        _actions.Add(new PlayerAttackAction());
        _actions.Add(new DefenseAction());
        _actions.Add(new SkillAction(Skills));
    }

    public override void BeginTurn()
    {
        base.BeginTurn();
    }

    public override void EndTurn()
    {
        base.EndTurn();
    }

    public override void PerformSkill(int skillPointCost)
    {
        SkillPoint -= skillPointCost;
        base.PerformSkill(skillPointCost);
    }
}
