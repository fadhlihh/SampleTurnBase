using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Beam Skill", menuName = "Game Data/Skill/Beam")]
public class BeamSkillData : SkillData
{
    public ParticleSystem VisualFX;
    public int DamagePoint;
    public override void Execute(TurnBasedCharacter instigator)
    {
        Debug.Log($"{instigator.Data.Name} Choose Skill {Name}");
        IEnumerable<TurnBasedCharacter> enemies = TurnBasedManager.Instance.GetAlliveEnemy();
        CameraManager.Instance.SwitchCamera(ECameraType.EnemyCamera, instigator);
        TargetSelector.Instance.StartSelectCharacter(enemies, target =>
                {
                    CameraManager.Instance.SwitchCamera(ECameraType.TargetCamera, target);
                    instigator.PerformSkill(SkillPoint);
                    Instantiate<ParticleSystem>(VisualFX, target.transform);
                    SFXManager.Instance.BeamSpellSFX?.Play();
                    target.Damage(DamagePoint);
                });
    }
}
