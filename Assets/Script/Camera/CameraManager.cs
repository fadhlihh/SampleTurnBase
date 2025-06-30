using System.Collections.Generic;
using Fadhli.Framework;
using UnityEngine;

public class CameraManager : SingletonBehaviour<CameraManager>
{
    [SerializeField]
    private List<GameCamera> _cameras = new List<GameCamera>();

    private GameCamera _currentActiveCamera;

    private void Start()
    {
        IEnumerable<GameCamera> nonDefaultCamera = _cameras.FindAll(camera => camera.Type != ECameraType.DefaultCamera);
        foreach (GameCamera camera in nonDefaultCamera)
        {
            camera.Deactivate();
        }
        GameCamera defaultCamera = _cameras.Find(camera => camera.Type == ECameraType.DefaultCamera);
        defaultCamera.Activate(null, null);
        _currentActiveCamera = defaultCamera;
    }

    public void SwitchCamera(ECameraType newCamera, TurnBasedCharacter instigator = null, TurnBasedCharacter target = null)
    {
        if (_currentActiveCamera)
        {
            _currentActiveCamera.Deactivate();
        }
        GameCamera camera = _cameras.Find(camera => camera.Type == newCamera);
        _currentActiveCamera = camera;
        _currentActiveCamera.Activate(instigator, target);
    }
}
