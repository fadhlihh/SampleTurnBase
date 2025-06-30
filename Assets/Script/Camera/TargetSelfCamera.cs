using Unity.Cinemachine;
using UnityEngine;

public class TargetSelfCamera : GameCamera
{
    public override void Activate(TurnBasedCharacter instigator, TurnBasedCharacter target)
    {
        Debug.Log(instigator.gameObject.name);
        Debug.Log(_camera.Target.TrackingTarget);
        _camera.Target.TrackingTarget = instigator.LookPivot;
        // _camera.Follow = instigator.LookPivot;
        base.Activate(instigator, target);
    }
}
