using System.Collections.Generic;
using UnityEngine;

public class EnemyItemAction : TurnBasedAction
{
    private List<Item> _items = new List<Item>();

    public EnemyItemAction()
    {
        Type = EActionCategory.Item;
    }

    public EnemyItemAction(List<ItemData> items)
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
        int randomIndex = Random.Range(0, _items.Count);
        _items[randomIndex].Data.Execute(instigator);
    }
}
