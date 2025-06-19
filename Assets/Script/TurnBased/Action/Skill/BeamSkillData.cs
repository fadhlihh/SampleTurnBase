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
        TargetSelector.Instance.StartSelectCharacter(enemies, target =>
                {
                    instigator.PerformSkill(SkillPoint);
                    Instantiate<ParticleSystem>(VisualFX, target.transform);
                    target.Damage(DamagePoint);
                });
    }
}
