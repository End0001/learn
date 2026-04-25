using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSlotData : MonoBehaviour
{
    public ItemData itemData;
    public int quantity;

    //构造函数
    public ItemSlotData(ItemData itemData,int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
        VaildateQuantity();
    }

    public ItemSlotData(ItemData itemData)
    {
        this.itemData= itemData;
        quantity = 1;
        VaildateQuantity();
    }

    //增加物品数量
    public void AddQuantity()
    {
        AddQuantity(1);
    }
    public void AddQuantity(int addQuantity)
    {
        quantity += addQuantity;
    }
    //减少一个物品数量
    public void RemoveQuantity()
    {
        quantity--;
        VaildateQuantity();
    }
    //清空
    private void VaildateQuantity()
    {
        if(quantity<=0||itemData==null)
        {
            EmptyQuantity();
        }
    }
    public void EmptyQuantity()
    {
        itemData = null;
        quantity = 0;
    }

}
