using Unity.Cinemachine;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    protected CinemachineCamera _camera;
    [SerializeField]
    private ECameraType _type;

    public ECameraType Type { get => _type; }

    public virtual void Activate(TurnBasedCharacter instigator, TurnBasedCharacter target)
    {
        _camera.Priority.Value = 1;
    }

    public void Deactivate()
    {
        _camera.Priority.Value = 0;
    }
}
