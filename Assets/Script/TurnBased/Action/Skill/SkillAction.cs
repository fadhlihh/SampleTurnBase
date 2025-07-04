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
        HUDManager.Instance.CharacterTurnUI.Hide();
        CameraManager.Instance.SwitchCamera(ECameraType.TrackingCamera, instigator);
        SkillMenuUI.Instance.Show(_skills, instigator, UseSkill);
    }

    public void UseSkill(SkillData skill, TurnBasedCharacter instigator)
    {
        skill.Execute(instigator);
    }
}
