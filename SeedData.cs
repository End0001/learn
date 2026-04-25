using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//种子
[CreateAssetMenu(menuName ="Items/Seed")]
public class SeedData : ItemData
{
    //农作物成长时间
    public int hoursToGrow;

    //初期
    public GameObject seed;

    //长成后
    public ItemData cropToYield;

    //成长期的幼苗
    public GameObject seeding;


}
