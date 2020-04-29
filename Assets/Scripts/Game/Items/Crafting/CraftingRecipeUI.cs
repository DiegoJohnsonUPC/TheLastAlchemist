using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeUI : ItemContainer
{

    public List<CraftingRecipe> recipes;

    public ItemContainer itemContainer;

    private CraftingRecipe craftingRecipe;
    public CraftingRecipe CraftingRecipe
    {
        get { return craftingRecipe; }
        set { SetCraftingRecipe(value); }
    }

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void OnValidate()
    {
        itemSlots = GetComponentsInChildren<ItemSlot>(includeInactive: true);
    }

    private void Start()
    {
        recipes.Reverse();
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        }
    }

    public void OnCraftButtonClick()
    {
        foreach (CraftingRecipe craftingRecipe in recipes)   
        {
            if (craftingRecipe.CanCraft(this))
            {
                if (!itemContainer.IsFull())
                {
                    craftingRecipe.Craft(itemContainer, this);
                }
                else
                {
                    Debug.Log("Inventory is Full!");
                }
                break;
            }
            else
            {
                Debug.Log("You don't have the required materials!");
            }
        }
    }

    private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
    {
        craftingRecipe = newCraftingRecipe;
        if(craftingRecipe != null)
        {
            int slotIndex = 0;
            slotIndex = SetSlots(craftingRecipe.Materials, slotIndex);
            slotIndex = SetSlots(craftingRecipe.Results, slotIndex);
        }
    }

    private int SetSlots(IList<ItemAmount> itemAmountList,int slotIndex)
    {
        for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
        {
            ItemAmount itemAmount = itemAmountList[i];
            ItemSlot itemSlot = itemSlots[slotIndex];

            itemSlot.item = itemAmount.Item;
            itemSlot.Amount = itemAmount.Amount;
        }
        return slotIndex;
    }
}

