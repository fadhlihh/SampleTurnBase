using UnityEngine;

public class TargetCamera : GameCamera
{
    [SerializeField]
    private Transform DefaultTarget;

    public override void Activate(TurnBasedCharacter instigator, TurnBasedCharacter target)
    {
        _camera.Target.TrackingTarget = instigator.TurnCameraPosition;
        if (instigator is PlayerCharacter)
        {
            _camera.Target.LookAtTarget = DefaultTarget;
        }
        else if (instigator is EnemyCharacter)
        {
            _camera.Target.LookAtTarget = instigator.LookPivot;
        }
        base.Activate(instigator, target);
    }
}
