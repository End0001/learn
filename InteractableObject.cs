using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //存储这个对象的类型
    public ItemData item;
    
    public void Pickup()
    {
        //设置类型
        InventoryManager.Instance.equippedItem = item;
        //捡到手上
        InventoryManager.Instance.RenderHand();
        //地上的消失
        Destroy(gameObject);
    }
}
