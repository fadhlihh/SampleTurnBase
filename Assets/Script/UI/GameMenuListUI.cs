using System;
using System.Collections.Generic;
using Fadhli.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuListUI<T> : SingletonBehaviour<GameMenuListUI<T>>
{
    [SerializeField]
    private GameObject _gameMenuList;
    [SerializeField]
    private Transform _gameMenuRect;
    [SerializeField]
    private GameMenuListItemUI<T> _gameMenuListItemPrefabs;
    [SerializeField]
    private TMP_Text _descriptionText;
    [SerializeField]
    private Image _titleBackground;
    [SerializeField]
    private Color _titleColor;
    [SerializeField]
    private TMP_Text _titleText;
    [SerializeField]
    private string _title;

    private List<GameMenuListItemUI<T>> _gameMenuListItems = new List<GameMenuListItemUI<T>>();

    public void Show(IEnumerable<T> itemDataList, TurnBasedCharacter instigator, Action<T, TurnBasedCharacter> onSelectItem)
    {
        foreach (T gameMenuListItem in itemDataList)
        {
            GameMenuListItemUI<T> item = Instantiate<GameMenuListItemUI<T>>(_gameMenuListItemPrefabs);
            item.transform.SetParent(_gameMenuRect, false);
            item.SetData(gameMenuListItem, instigator, (item, instigator) =>
            {
                onSelectItem?.Invoke(item, instigator);
                OnSelectItem();
            }, OnHoverItem, OnExitItem);
            _gameMenuListItems.Add(item);
        }
        onSelectItem += (item, instigator) => OnSelectItem();
        _titleText.text = _title;
        _titleBackground.color = _titleColor;
        _gameMenuList.SetActive(true);
    }

    public void Hide()
    {
        ClearItems();
        _gameMenuList.SetActive(false);
    }

    public void OnSelectItem()
    {
        Hide();
    }

    public void OnHoverItem(string description)
    {
        _descriptionText.text = description;
    }

    public void OnExitItem()
    {
        _descriptionText.text = "";
    }

    public void ClearItems()
    {
        Debug.Log("Clear");
        foreach (GameMenuListItemUI<T> item in _gameMenuListItems)
        {
            Destroy(item.gameObject);
        }
        _gameMenuListItems.Clear();
    }

    public void Cancel()
    {
        Hide();
        TurnBasedManager.Instance.CancelAction();
    }
}
