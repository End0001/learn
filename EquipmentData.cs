using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//工具也有物品的那些属性，直接继承
[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentData : ItemData
{
    public enum ToolType
    {
        //锄头、浇水壶、斧头、镐子
        Hoe, WateringCan, Axe, Pickaxe
    }
    public ToolType toolType;
}
