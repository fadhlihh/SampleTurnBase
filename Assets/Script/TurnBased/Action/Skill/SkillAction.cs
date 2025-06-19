using System.Collections.Generic;
using UnityEngine;

public class SkillAction : TurnBasedAction
{
    private List<SkillData> _skills = new List<SkillData>();

    public SkillAction()
    {
        Type = EActionCategory.Skill;
    }

    public SkillAction(List<SkillData> skills)
    {
        Type = EActionCategory.Skill;
        _skills = skills;
    }


    public override void Execute(TurnBasedCharacter instigator)
    {
        SkillMenuUI.Instance.Show(_skills, instigator);
    }
}
