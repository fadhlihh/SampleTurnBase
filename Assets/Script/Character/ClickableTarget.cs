using System;
using UnityEngine;
using UnityEngine.Events;

public class ClickableTarget : MonoBehaviour
{
    public TurnBasedCharacter _ownerCharacter;

    public UnityEvent<TurnBasedCharacter> OnClicked;

    private void OnMouseDown()
    {
        Debug.Log("Click");
        OnClicked?.Invoke(_ownerCharacter);
    }
}
