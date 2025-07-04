using System;
using UnityEngine;
using UnityEngine.Events;

public class ClickableTarget : MonoBehaviour
{
    [SerializeField]
    private TurnBasedCharacter _ownerCharacter;

    public bool IsSelecting { get; set; }

    public UnityEvent<TurnBasedCharacter> OnClicked;

    private void OnMouseDown()
    {
        Debug.Log("Click");
        OnClicked?.Invoke(_ownerCharacter);
        _ownerCharacter.SelectorUI.Hide();
    }

    private void OnMouseEnter()
    {
        if (IsSelecting)
        {
            _ownerCharacter.SelectorUI.ShowSelector();
        }
    }

    private void OnMouseExit()
    {
        if (TurnBasedManager.Instance.CurrentCharacter == _ownerCharacter)
        {
            _ownerCharacter.SelectorUI.ShowTurnIcon();
        }
        else
        {
            _ownerCharacter.SelectorUI.Hide();
        }
    }
}
