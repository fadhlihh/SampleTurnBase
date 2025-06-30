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
        _actions.Add(new ItemAction(Items));
    }

    public override void BeginTurn()
    {
        base.BeginTurn();
    }

    public override void EndTurn()
    {
        base.EndTurn();
        HUDManager.Instance.CharacterTurnUI.Show();
    }

    public override void PerformSkill(int skillPointCost)
    {
        SkillPoint -= skillPointCost;
        base.PerformSkill(skillPointCost);
    }
}
