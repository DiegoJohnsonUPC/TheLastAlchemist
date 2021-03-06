﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] Image image;
    [SerializeField] Text amountText;

    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1,1,1,0);

    private Item _item;
    public Item item{
        get { return _item; }
        set {
            _item = value;

            if(_item == null){
                image.color = disabledColor;
            } else{
                image.sprite = _item.Icon;
                image.color = normalColor;
            }
        }
    }
    
    private int _amount;
    public int Amount
    {
        get { return _amount;}
        set
        {
            _amount = value;
            if (_amount < 0) _amount = 0;
            if (_amount == 0) item = null;

            amountText.enabled = _item != null && _amount > 1;
            if (amountText.enabled)
            {
                amountText.text = _amount.ToString();
            }   
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
                OnRightClickEvent(this);
        }
    }

    protected virtual void OnValidate()
    {
        if(_item == null)
            image.color = disabledColor;
        if (image == null)
            image = GetComponent<Image>();
        if (amountText == null)
            amountText = GetComponentInChildren<Text>();
    }

    public virtual bool CanAddStack(Item Iitem, int amount = 1)
    {
        return item != null && item.ID == Iitem.ID && Amount + amount <=  Iitem.MaximumStack;
    }

    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
            OnPointerEnterEvent(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
            OnPointerExitEvent(this);
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
            OnBeginDragEvent(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
            OnEndDragEvent(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
            OnDragEvent(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
            OnDropEvent(this);
    }
}

