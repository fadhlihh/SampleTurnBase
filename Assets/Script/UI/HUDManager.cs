using Fadhli.Framework;
using UnityEngine;

public class HUDManager : SingletonBehaviour<HUDManager>
{
    [SerializeField]
    private PlayerActionUI _playerActionUI;
    [SerializeField]
    private PlayerStatusUI _playerStatusUI;
    [SerializeField]
    private CharacterTurnUI _characterTurnUI;

    public PlayerActionUI PlayerActionUI { get => _playerActionUI; }
    public PlayerStatusUI PlayerStatusUI { get => _playerStatusUI; }
    public CharacterTurnUI CharacterTurnUI { get => _characterTurnUI; }
}
