using System.Collections.Generic;
using UnityEngine;

public class EnemySkillAction : TurnBasedAction
{
    private List<SkillData> _skills = new List<SkillData>();

    public EnemySkillAction()
    {
        Type = EActionCategory.Skill;
    }

    public EnemySkillAction(List<SkillData> skills)
    {
        Type = EActionCategory.Skill;
        _skills = skills;
    }


    public override void Execute(TurnBasedCharacter instigator)
    {
        int randomIndex = Random.Range(0, _skills.Count);
        _skills[randomIndex].Execute(instigator);
    }
}
