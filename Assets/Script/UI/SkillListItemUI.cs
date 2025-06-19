using System;
using TMPro;
using UnityEngine;

public class SkillListItemUI : GameMenuListItemUI<SkillData>
{
    [SerializeField]
    private TMP_Text _nameText;
    [SerializeField]
    private TMP_Text _skillPointText;

    public override void SetData(SkillData data, TurnBasedCharacter instigator, Action onSelectItem, Action<string> onHoverItem, Action onExitItem)
    {
        _nameText.text = data.Name;
        _skillPointText.text = data.SkillPoint.ToString();
        if (instigator.SkillPoint < data.SkillPoint)
        {
            IsDisabled = true;
            _disabledOverlay.SetActive(true);
        }
        else
        {
            IsDisabled = false;
            _disabledOverlay.SetActive(false);
        }
        Description = data.Description;
        OnSelectItem += () => data.Execute(instigator);
        OnSelectItem += onSelectItem;
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
