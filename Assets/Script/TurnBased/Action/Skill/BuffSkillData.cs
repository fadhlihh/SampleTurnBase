using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff Skill", menuName = "Game Data/Skill/Buff")]
public class BuffSkillData : SkillData
{
    public ParticleSystem VisualFX;
    public EStatType BuffedStat;
    public int Amount;
    public int Duration;
    public ETargetingMode TargetingMode;

    public override void Execute(TurnBasedCharacter instigator)
    {
        Debug.Log($"{instigator.Data.Name} Choose Skill {Name}");
        switch (TargetingMode)
        {
            case ETargetingMode.Self:
                instigator.ApplyBuff(BuffedStat, Amount, Duration);
                Instantiate<ParticleSystem>(VisualFX, instigator.transform);
                SFXManager.Instance.BuffSpellSFX?.Play();
                Debug.Log($"{instigator.Data.Name} Apply Skill {Name} on self");
                CameraManager.Instance.SwitchCamera(ECameraType.TargetCamera, instigator);
                instigator.PerformSkill(SkillPoint);
                break;
            case ETargetingMode.ManualSelectionAlly:
                IEnumerable<TurnBasedCharacter> targets = instigator is PlayerCharacter ?
                TurnBasedManager.Instance.GetAllivePlayer() :
                TurnBasedManager.Instance.GetAlliveEnemy();
                if (instigator is PlayerCharacter)
                {
                    CameraManager.Instance.SwitchCamera(ECameraType.AllyCamera, instigator);
                }
                TargetSelector.Instance.StartSelectCharacter(targets, target =>
                {
                    CameraManager.Instance.SwitchCamera(ECameraType.TargetCamera, target);
                    target.ApplyBuff(BuffedStat, Amount, Duration);
                    Instantiate<ParticleSystem>(VisualFX, target.transform);
                    SFXManager.Instance.BuffSpellSFX?.Play();
                    Debug.Log($"{instigator.Data.Name} Apply Skill {Name} on {target}");
                    instigator.PerformSkill(SkillPoint);
                });
                break;
            default:
                break;
        }
    }
}
