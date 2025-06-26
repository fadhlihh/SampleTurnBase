using System;
using TMPro;
using UnityEngine;

public class ItemListItemUI : GameMenuListItemUI<Item>
{
    [SerializeField]
    private TMP_Text _nameText;
    [SerializeField]
    private TMP_Text _quantityText;

    public override void SetData(Item data, TurnBasedCharacter instigator, Action<Item, TurnBasedCharacter> onSelectItem, Action<string> onHoverItem, Action onExitItem)
    {
        _nameText.text = data.Data.Name;
        _quantityText.text = data.Quantity.ToString();
        Description = data.Data.Description;
        if (data.Quantity <= 0)
        {
            IsDisabled = true;
            _disabledOverlay.SetActive(true);
        }
        else
        {
            IsDisabled = false;
            _disabledOverlay.SetActive(false);
        }
        OnSelectItem += () => onSelectItem(data, instigator);
        OnHoverItem = onHoverItem;
        OnExitItem = onExitItem;
    }

    public void OnSelect()
    {
        if (!IsDisabled)
        {
            OnSelectItem?.Invoke();
        }
    }

    public void OnHover()
    {
        OnHoverItem?.Invoke(Description);
    }

    public void OnExit()
    {
        OnExitItem?.Invoke();
    }

    private void OnDisable()
    {
        OnSelectItem = null;
    }
}
