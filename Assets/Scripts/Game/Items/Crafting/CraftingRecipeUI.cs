using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeUI : MonoBehaviour
{

    [SerializeField] ItemSlot[] itemSlots;

    public ItemContainer ItemContainer;

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
        foreach (ItemSlot itemSlot in itemSlots)
        {
            itemSlot.OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlot.OnPointerExitEvent += OnPointerExitEvent;
            itemSlot.OnBeginDragEvent += OnBeginDragEvent;
            itemSlot.OnEndDragEvent += OnEndDragEvent;
            itemSlot.OnDragEvent += OnDragEvent;
            itemSlot.OnDropEvent += OnDropEvent;
        }            
    }

    public void OnCraftButtonClick()
    {
        if(craftingRecipe != null && ItemContainer != null)
        {
            if (craftingRecipe.CanCraft(ItemContainer))
            {
                if (!ItemContainer.IsFull())
                {
                    craftingRecipe.Craft(ItemContainer);
                }
                else
                {
                    Debug.LogError("Inventory is Full!");
                }
            }
            else
            {
                Debug.LogError("You don't have the required materials!");
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

