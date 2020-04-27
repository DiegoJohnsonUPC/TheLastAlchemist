using UnityEngine;
using TLA.CharacterStats;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public CharacterStat Damage;

    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] CraftingRecipeUI craftingRecipeUI;
    [SerializeField] Image draggableItem;

    private ItemSlot dragItemSlot;

    private void OnValidate()
    {
        if (itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemTooltip>();
    }

    private void Start()
    {
        statPanel.SetStats(Damage);
        statPanel.UpdateStatValues();

        //Setup Events: 
        //Right Click
        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += Unequip;
        //Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;
        craftingRecipeUI.OnPointerEnterEvent += ShowTooltip;
        //Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
        craftingRecipeUI.OnPointerExitEvent += HideTooltip;
        //Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        craftingRecipeUI.OnBeginDragEvent += BeginDrag;
        //End Drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        craftingRecipeUI.OnEndDragEvent += EndDrag;
        //Drag
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        craftingRecipeUI.OnDragEvent += Drag;
        //Drop
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
        craftingRecipeUI.OnDropEvent += Drop;
    }

    private void Equip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.item as EquipableItem;
        if(equipableItem != null)
        {
            Equip(equipableItem);
        }
    }

    private void Unequip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.item as EquipableItem;
        if (equipableItem != null)
        {
            Unequip(equipableItem);
        }
    }

    private void ShowTooltip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.item as EquipableItem;
        if (equipableItem != null)
        {
            itemTooltip.ShowTooltip(equipableItem);
        }
    }

    private void HideTooltip(ItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if(itemSlot.item != null)
        {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        dragItemSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(ItemSlot itemSlot)
    {
        if(draggableItem.enabled)
            draggableItem.transform.position = Input.mousePosition;
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if (dragItemSlot == null) return;

        if (dropItemSlot.CanAddStack(dragItemSlot.item))
        {
            AddStacks(dropItemSlot);
        }
        else if(dropItemSlot.CanReceiveItem(dragItemSlot.item) && dragItemSlot.CanReceiveItem(dropItemSlot.item))
        {
            SwapItems(dropItemSlot);
        }

    }

    private void SwapItems(ItemSlot dropItemSlot)
    {
        EquipableItem dragItem = dragItemSlot.item as EquipableItem;
        EquipableItem dropItem = dropItemSlot.item as EquipableItem;

        if (dragItemSlot is EquipmentSlot)
        {
            if (dragItem != null) dragItem.Unequip(this);
            if (dropItem != null) dropItem.Equip(this);
        }
        if (dropItemSlot is EquipmentSlot)
        {
            if (dragItem != null) dragItem.Equip(this);
            if (dropItem != null) dropItem.Unequip(this);
        }
        statPanel.UpdateStatValues();

        Item draggedItem = dragItemSlot.item;
        int draggedItemAmount = dragItemSlot.Amount;

        dragItemSlot.item = dropItemSlot.item;
        dragItemSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.item = draggedItem;
        dropItemSlot.Amount = draggedItemAmount;
    }

    private void AddStacks(ItemSlot dropItemSlot)
    {
        int numAddableStacks = dropItemSlot.item.MaximumStack - dropItemSlot.Amount;
        int stacksToAdd = Mathf.Min(numAddableStacks, dragItemSlot.Amount);

        dropItemSlot.Amount += stacksToAdd;
        dragItemSlot.Amount -= stacksToAdd;
    }

    public void Equip(EquipableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquipableItem previousItem;
            if(equipmentPanel.AddItem(item, out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(EquipableItem item)
    {
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }
}
