using System.Collections.Generic;
using UnityEngine;

public class ItemAction : TurnBasedAction
{
    private List<Item> _items = new List<Item>();

    public ItemAction()
    {
        Type = EActionCategory.Item;
    }

    public ItemAction(List<ItemData> items)
    {
        Type = EActionCategory.Item;
        foreach (ItemData itemData in items)
        {
            Item item = null;
            if (_items.Count > 0)
            {
                item = _items.Find(context => string.Equals(context.Data.Name, itemData.Name));
            }
            if (item != null)
            {
                item.Quantity += 1;
            }
            else
            {
                Item newItem = new Item(itemData, 1);
                _items.Add(newItem);
            }
        }
    }

    public override void Execute(TurnBasedCharacter instigator)
    {
        CameraManager.Instance.SwitchCamera(ECameraType.TrackingCamera, instigator);
        ItemMenuUI.Instance.Show(_items, instigator, UseItem);
    }

    public void UseItem(Item item, TurnBasedCharacter instigator)
    {
        item.Data.Execute(instigator);
        if (instigator is PlayerCharacter)
        {
            item.Quantity -= 1;
        }
    }
}
