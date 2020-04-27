using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
    public Item Item;
    [Range(1,999)]
    public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    public bool CanCraft(IItemContainer itemContainerEntrada)
    {
        foreach (ItemAmount itemAmount in Materials)
        {
            if(itemContainerEntrada.ItemCount(itemAmount.Item.ID) < itemAmount.Amount)
            {
                return false;
            }
        }
        return true;
    }

    public void Craft(IItemContainer itemContainerSalida, IItemContainer itemContainerEntrada)
    {
        if (CanCraft(itemContainerEntrada))
        {
            foreach (ItemAmount itemAmount in Materials)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    Item oldItem = itemContainerEntrada.RemoveItem(itemAmount.Item.ID);
                    oldItem.Destroy();
                }
            }

            foreach (ItemAmount itemAmount in Results)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    itemContainerSalida.AddItem(itemAmount.Item.GetCopy());
                }
            }
        }
    }

}
