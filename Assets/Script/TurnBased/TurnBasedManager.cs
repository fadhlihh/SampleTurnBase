using System.Collections.Generic;
using System.Linq;
using Fadhli.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnBasedManager : SingletonBehaviour<TurnBasedManager>
{
    [SerializeField]
    protected List<TurnBasedCharacter> _characters = new List<TurnBasedCharacter>();
    [SerializeField]
    protected CharacterTurnUI _characterTurnUI;
    [SerializeField]
    protected PlayerStatusUI _playerStatusUI;

    private Queue<TurnBasedCharacter> _characterTurn = new Queue<TurnBasedCharacter>();
    private TurnBasedCharacter _currentCharacter;

    public List<TurnBasedCharacter> Characters { get => _characters; }
    public TurnBasedCharacter CurrentCharacter { get => _currentCharacter; }

    protected virtual void Start()
    {
        BindAllCharacterEvent();
        _characterTurnUI.InitializeTurnItem(Characters);
        _playerStatusUI.InitializePlayerStatusItem(GetAllivePlayer());
        EnqueueTurn();
        NextTurn();
    }

    public void BindAllCharacterEvent()
    {
        foreach (TurnBasedCharacter character in _characters)
        {
            character.OnCharacterDeath.AddListener(HandlePlayerDeath);
        }
    }

    public void CancelAction()
    {
        CameraManager.Instance.SwitchCamera(ECameraType.TargetCamera, _currentCharacter);
        HUDManager.Instance.PlayerActionUI.Show();
        _currentCharacter.SelectorUI.ShowTurnIcon();
    }

    public void NextTurn()
    {
        if (_characterTurn.Count <= 0)
        {
            EnqueueTurn();
        }
        _characterTurnUI.HideTurnIconFor(_currentCharacter);
        _currentCharacter = _characterTurn.Dequeue();
        if (!_currentCharacter.IsDead)
        {
            _currentCharacter.BeginTurn();
            _characterTurnUI.ShowTurnIconFor(_currentCharacter);
            if (_currentCharacter is PlayerCharacter)
            {
                HUDManager.Instance.PlayerActionUI.Show();
            }
            else
            {
                HUDManager.Instance.PlayerActionUI.Hide();
            }
        }
        else
        {
            NextTurn();
        }
    }

    public void EnqueueTurn()
    {
        _characters.Sort((a, b) => b.Speed.CompareTo(a.Speed));
        _characterTurn.Clear();

        Debug.Log("Enqueue new turn");

        foreach (TurnBasedCharacter character in _characters)
        {
            if (!character.IsDead)
            {
                _characterTurn.Enqueue(character);
            }
        }

        _characterTurnUI.UpdateTurnOrderUI(_characters);
    }

    public List<EnemyCharacter> GetAlliveEnemy()
    {
        return _characters.FindAll(item => !item.IsDead).OfType<EnemyCharacter>().ToList();
    }

    public List<PlayerCharacter> GetAllivePlayer()
    {
        return _characters.FindAll(item => !item.IsDead).OfType<PlayerCharacter>().ToList();
    }

    public void HandlePlayerAction(EActionCategory type)
    {
        HUDManager.Instance.PlayerActionUI.Hide();
        TurnBasedAction action = _currentCharacter.Actions.Find(item => item.Type == type);
        if (action != null)
        {
            action.Execute(_currentCharacter);
            _currentCharacter.SelectorUI.Hide();
        }
    }

    public void HandlePlayerDeath(TurnBasedCharacter character)
    {
        _characterTurnUI.ShowDeadOverlayFor(character);
        CheckGameOver();
    }

    public bool IsAllPlayerDead()
    {
        return GetAllivePlayer().Count <= 0;
    }

    public bool IsAllEnemyDead()
    {
        return GetAlliveEnemy().Count <= 0;
    }

    public void CheckGameOver()
    {
        if (IsAllEnemyDead())
        {
            SceneManager.LoadScene("WinScreen");
        }

        if (IsAllPlayerDead())
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
