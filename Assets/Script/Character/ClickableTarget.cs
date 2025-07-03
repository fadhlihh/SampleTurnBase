using System;
using UnityEngine;
using UnityEngine.Events;

public class ClickableTarget : MonoBehaviour
{
    [SerializeField]
    private TurnBasedCharacter _ownerCharacter;
    [SerializeField]
    private SelectorUI _selectorUI;

    public bool IsSelecting { get; set; }

    public UnityEvent<TurnBasedCharacter> OnClicked;

    private void OnMouseDown()
    {
        Debug.Log("Click");
        OnClicked?.Invoke(_ownerCharacter);
        _selectorUI.Hide();
    }

    private void OnMouseEnter()
    {
        if (IsSelecting)
        {
            _selectorUI.Show();
        }
    }

    private void OnMouseExit()
    {
        _selectorUI.Hide();
    }
}
