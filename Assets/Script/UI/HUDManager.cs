using Fadhli.Framework;
using UnityEngine;

public class HUDManager : SingletonBehaviour<HUDManager>
{
    [SerializeField]
    private PlayerActionUI _playerActionUI;

    public PlayerActionUI PlayerActionUI { get => _playerActionUI; }
}
