using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    //将要成长的样子
    SeedData seedToGrow;

    [Header("成长周期样式")]
    public GameObject seed;
    public GameObject seeding;
    public GameObject harvestable;

    //成长阶段
    int growth;
    //总成长时间（分钟）
    int maxGrowth;

    public enum CropState
    {
        Seed, Seeding, Harvestable
    }
    public CropState cropState;

    //
    public void Plant(SeedData seedToGrow)
    {
        //保存
        this.seedToGrow = seedToGrow;
        //创建新的
        seed = Instantiate(seedToGrow.seed, transform);
        
        seeding = Instantiate(seedToGrow.seeding, transform);
        //获取下一阶段预制体的数据
        ItemData cropToYield = seedToGrow.cropToYield;
        //创建下一阶段对象
        harvestable=Instantiate(cropToYield.gameModel,transform);
        //成长时间
        maxGrowth = GameTimeStamp.HoursToMinutes(seedToGrow.hoursToGrow);
        //设置
        SwitchState(CropState.Seed);
    }

    public void Grow()
    {
        growth++;

        //从种子阶段进入下一阶段
        if(growth>=maxGrowth/2 && cropState==CropState.Seed)
        {
            SwitchState(CropState.Seeding);
        }
        //从幼苗阶段进入下一阶段
        if(growth>=maxGrowth && cropState==CropState.Seeding)
        {
            SwitchState (CropState.Harvestable);
        }

    }

    //显示某阶段的模型
    public void SwitchState(CropState stateToSwitch)
    {
        
        seed.SetActive(false);
        seeding.SetActive(false);
        harvestable.SetActive(false);
        //在哪个阶段就显示哪个模型
        switch (stateToSwitch)
        {
            case CropState.Seed:
                seed.SetActive(true); 
                break;
            case CropState.Seeding:
                seeding.SetActive(true); 
                break;
            case CropState.Harvestable: 
                harvestable.SetActive(true);
                break;
        }
        cropState = stateToSwitch;
    }
}
