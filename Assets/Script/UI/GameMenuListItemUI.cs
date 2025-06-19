using System;
using UnityEngine;

public abstract class GameMenuListItemUI<T> : MonoBehaviour
{
    [SerializeField]
    protected GameObject _disabledOverlay;

    public Action OnSelectItem;
    public Action<string> OnHoverItem;
    public Action OnExitItem;

    public string Description { get; protected set; }
    public bool IsDisabled { get; protected set; }

    public abstract void SetData(T data, TurnBasedCharacter instigator, Action onSelectItem, Action<string> onHoverItem, Action onExitItem);
}
