using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//物品数据
//选用ScriptableObject可以让数据持久保存（一个项目而不是一个场景）
//为项目设置一个可以创建的对象名称
[CreateAssetMenu(menuName ="Items/Item")]
public class ItemData : ScriptableObject
{
    //物品的描述
    public string description;
    //物品图标
    public Sprite thumbnail;
    //游戏内显示的对象
    public GameObject gameModel;

}
