using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Land : MonoBehaviour,ITimeTrack
{
    //土地纹理的枚举
    public enum LandStatus
    {
        Dirt,DryLand,WetLand
    }

    public LandStatus landStatus;
    
    //土地材质
    public Material dirtMat, dryMat, wetMat;
    new Renderer renderer;

    //被选择的土地
    public GameObject select;

    //浇水的土地干涸
    GameTimeStamp timeWatered;

    [Header("Crops")]
    public GameObject cropPrefab;
    CropBehavior cropPlanted=null;//这个土地上的植物

    // Start is called before the first frame update
    void Start()
    {
        //初始化
        renderer = GetComponent<Renderer>();

        //设置默认土地材质
        SwitchLandStatus(LandStatus.Dirt);

        //设置默认状态
        Select(false);

        //把this加入接收信息的队列
        TimeManager.Instance.RegisterTracker(this);
    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        
        landStatus = statusToSwitch;

        Material materialToSwitch = dirtMat;

        //设置材质
        switch(statusToSwitch)
        {
            case LandStatus.Dirt:
                materialToSwitch=dirtMat; 
                break;
            case LandStatus.DryLand:
                materialToSwitch=dryMat;
                break;
            case LandStatus.WetLand:               
                materialToSwitch = wetMat;               
                timeWatered=TimeManager.Instance.GetGameTimeStamp();
                break;
        }
        //应用修改好的材质
        renderer.material = materialToSwitch;

    }
    public void Select(bool isActive)
    {
        select.SetActive(isActive);
    }

    //玩家交互时更改土地
    public void Interact()
    {
        //检查玩家手里工具是什么
        ItemData toolSlot = InventoryManager.Instance.equippedTool;
        if (toolSlot==null)
        {
            return;
        }

        //转化成工具数据类型
        EquipmentData equipmentTool = toolSlot as EquipmentData;
        
        //不同工具有不同作用
        if( equipmentTool != null )
        {
            //得到类型
            EquipmentData.ToolType toolType = equipmentTool.toolType;

            switch(toolType)
            {
                case EquipmentData.ToolType.Hoe:
                    SwitchLandStatus(LandStatus.DryLand);
                    break;
                case EquipmentData.ToolType.WateringCan:
                    if (landStatus == LandStatus.DryLand)
                    {
                        SwitchLandStatus(LandStatus.WetLand);
                    }
                    break;
            }

            return;
        }
        //检测工具
        SeedData seedTool=toolSlot as SeedData;

        if(seedTool!=null && landStatus != LandStatus.Dirt && cropPlanted==null)
        {
            //创建对象
            GameObject cropObject = Instantiate(cropPrefab, transform);
            
            //获取要种植的种子信息
            cropPlanted=cropObject.GetComponent<CropBehavior>();
            //种植
            cropPlanted.Plant(seedTool);

        }
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        //游戏内2小时后土地变干
        if(landStatus == LandStatus.WetLand)
        {
            //计算浇水后的时间
            int hoursWet = GameTimeStamp.CompareTimeStamps(timeWatered, timeStamp);

            //种子成长
            if(cropPlanted!=null)
            {
                //cropPlanted.gameObject.SetActive(true);
                cropPlanted.Grow();
            }

            if(hoursWet >6)
            {
                //土地干涸
                SwitchLandStatus(LandStatus.DryLand);
            }
        }
    }
}
