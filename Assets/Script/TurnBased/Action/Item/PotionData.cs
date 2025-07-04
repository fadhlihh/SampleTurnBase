using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion Item", menuName = "Game Data/Item/Potion")]
public class PotionData : ItemData
{
    public ParticleSystem VisualFX;
    public int Amount;
    public EPotionEffectType EffectType;
    public ETargetingMode TargetingMode;

    public override void Execute(TurnBasedCharacter instigator)
    {
        switch (TargetingMode)
        {
            case ETargetingMode.Self:
                Consume(instigator, instigator);
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
                    Consume(instigator, target);
                });
                break;
            default:
                break;
        }
    }

    public void Consume(TurnBasedCharacter instigator, TurnBasedCharacter target)
    {
        Instantiate<ParticleSystem>(VisualFX, target.transform);
        switch (EffectType)
        {
            case EPotionEffectType.Heal:
                target.Heal(Amount);
                break;
            case EPotionEffectType.RestoreSkillPoint:
                target.RestoreSkillPoint(Amount);
                break;
            default:
                break;
        }
        instigator.EndTurnWithDelay(3);
    }
}

