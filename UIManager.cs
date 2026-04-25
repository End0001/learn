using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//处理UI显示
public class UIManager : MonoBehaviour,ITimeTrack
{
    public static UIManager Instance { get; private set; }

    [Header("Status Bar")]
    //玩家手里的工具(的图片)
    public Image handSlotImage;
    //时间UI
    public Text timeText;
    public Text dateText;

    [Header("Inventory System")]
    //背包界面
    public GameObject inventoryPanel;
    //存储物品栏、工具栏
    public InventorySlot[] toolSlots;
    public InventorySlot[] itemSlots;
    //物品详情：名、简介
    public Text itemNameText;
    public Text itemDescriptionText;

    //手里的工具、物品
    public HandInventorySlot toolHandSlot;
    public HandInventorySlot itemHandSlot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void RenderInventory()
    {
        //分别得到工具和物品栏的数组
        ItemData[] inventoryToolSlots = InventoryManager.Instance.tools;
        ItemData[] inventoryItemSlots = InventoryManager.Instance.items;

        //更新物品栏和工具栏的图片
        RenderInventoryPanel(inventoryToolSlots, toolSlots);
        RenderInventoryPanel(inventoryItemSlots, itemSlots);

        //更新手里的装备栏
        toolHandSlot.Display(InventoryManager.Instance.equippedTool);
        itemHandSlot.Display(InventoryManager.Instance.equippedItem);

        //得到玩家手里的工具
        ItemData equippedTool = InventoryManager.Instance.equippedTool;
        //有图片显示图片
        if (equippedTool != null)
        {
            handSlotImage.sprite = equippedTool.thumbnail;

            handSlotImage.gameObject.SetActive(true);
            return;
        }
        //没有图片时不显示
        handSlotImage.gameObject.SetActive(false);
    }

    //将场景内物品栏、工具栏赋值给这里的数组
    void RenderInventoryPanel(ItemData[] slots,InventorySlot[] uiSlots)
    {
        for(int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Display(slots[i]);
        }
    }

    //更新物品栏序列
    public void AssighSlotIndexs()
    {
        for(int i = 0;i<toolSlots.Length;i++)
        {
            toolSlots[i].AssignIndex(i);
            itemSlots[i].AssignIndex(i);
        }
    }

    private void Start()
    {
        RenderInventory();
        AssighSlotIndexs();

        //加入接收消息的队列
        TimeManager.Instance.RegisterTracker(this);
    }

    //打开或关闭背包界面、并更新内部物品栏。
    public void ActivePanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        RenderInventory();
    }

    //显示物品详情
    public void DisplayItemInfo(ItemData data)
    {
        if(data == null)
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";
            return;
        }
        itemNameText.text = data.name;
        itemDescriptionText.text = data.description;
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        //得到时间
        int hours = timeStamp.hour;
        int minutes = timeStamp.minute;

        //AM或PM
        string prefix = "AM";

        //时间显示
        if(hours > 12) 
        {
            prefix = "PM";
            hours -= 12;
        }

        timeText.text = prefix + hours +":"+ minutes.ToString("00");
    
        //日期显示
        int day=timeStamp.day;
        string season=timeStamp.season.ToString();
        string dayOfWeek=timeStamp.GetDayOfTheWeek().ToString();
        dateText.text = season + " " + day + " (" + dayOfWeek + " )";
    }
}
