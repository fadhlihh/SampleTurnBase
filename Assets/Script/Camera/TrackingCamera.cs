using Unity.Cinemachine;
using UnityEngine;

public class TrackingCamera : GameCamera
{
    public override void Activate(TurnBasedCharacter instigator, TurnBasedCharacter target)
    {
        _camera.Target.TrackingTarget = instigator.LookPivot;
        // _camera.Follow = instigator.LookPivot;
        base.Activate(instigator, target);
    }
}
