using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//处理背包栏--从手上转移到背包栏里。
public class HandInventorySlot : InventorySlot
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        
        InventoryManager.Instance.HandToInventory(inventoryType);
    }
}
