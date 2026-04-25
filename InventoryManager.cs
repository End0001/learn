using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//管理物品栏、工具栏、手上栏
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance!=null && Instance!=this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [Header("Tools")]
    //工具栏
    public ItemData[] tools=new ItemData[8];
    //玩家手里的工具
    public ItemData equippedTool = null;

    [Header("Items")]
    //物品栏
    public ItemData[] items = new ItemData[8];
    //玩家手里的物品
    public ItemData equippedItem = null;

    //右手拿东西的位置
    public Transform handPoint;


    //从物品栏到手里
    public void InventoryToHand(int slotIndex,InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType==InventorySlot.InventoryType.Item)
        {
            //将鼠标点击的物品与手上的交换
            ItemData itemToEquip = items[slotIndex];
            items[slotIndex] = equippedItem;
            equippedItem=itemToEquip;
            //显示手上的物品
            RenderHand();
        }
        else
        {
            //将鼠标点击的物品与手上的交换
            ItemData toolToEquip = tools[slotIndex];
            tools[slotIndex] = equippedTool;
            equippedTool = toolToEquip;
        }

        UIManager.Instance.RenderInventory();
    }
    //从手到物品栏
    public void HandToInventory(InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            //循环找到第一个空格子，把手里的赋值给空格子
            for(int i = 0; i < items.Length; i++)
            {
                if (items[i]==null)
                {
                    items[i] = equippedItem;
                    //手里的格子置空
                    equippedItem = null;
                    break;
                }
            }
            //显示手上的物品
            RenderHand();
        }
        else
        {
            //循环找到第一个空格子，把手里的赋值给空格子
            for (int i = 0; i < tools.Length; i++)
            {
                if (tools[i] == null)
                {
                    tools[i] = equippedTool;
                    //手里的格子置空
                    equippedTool = null;
                    break;
                }
            }
            
        }
        UIManager.Instance.RenderInventory();
    }

    public void RenderHand()
    {
        if(handPoint.childCount>0)
        {
            Destroy(handPoint.GetChild(0).gameObject);
        }
        if(equippedItem != null)
        {
            Instantiate(equippedItem.gameModel, handPoint);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
