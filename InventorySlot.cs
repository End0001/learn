using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//工具栏的图片显示相关
public class InventorySlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    ItemData itemToDisplay;
    public Image itemDisplayImage;

    //用于识别工具的类型
    public enum InventoryType
    {
        Item,Tool
    }
    public InventoryType inventoryType;

    //索引
    int slotIndex;

    public void Display(ItemData itemToDisplay)
    {
        //有图片显示图片
        if (itemToDisplay != null)
        {
            itemDisplayImage.sprite = itemToDisplay.thumbnail;
            this.itemToDisplay = itemToDisplay;

            itemDisplayImage.gameObject.SetActive(true);
            return;
        }
        //没有图片时不显示
        itemDisplayImage.gameObject.SetActive(false);
    }

    //设置物品栏序列索引
    public void AssignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }

    //鼠标指针坐标在this的坐标时
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(itemToDisplay);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(null);
    }

    //鼠标指针点击this
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.Instance.InventoryToHand(slotIndex,inventoryType);
    }
}
