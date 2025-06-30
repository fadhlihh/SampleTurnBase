using Unity.Cinemachine;
using UnityEngine;

public class TargetGroupCamera : GameCamera
{
    [SerializeField]
    private CinemachineTargetGroup _targetGroup;

    public override void Activate(TurnBasedCharacter instigator, TurnBasedCharacter target)
    {
        for (int i = 0; i < _targetGroup.Targets.Count; i++)
        {
            _targetGroup.RemoveMember(_targetGroup.Targets[0].Object);
        }
        _targetGroup.AddMember(instigator.LookPivot, 1, 0.5f);
        _targetGroup.AddMember(target.LookPivot, 1, 0.5f);
        base.Activate(instigator, target);
    }
}
