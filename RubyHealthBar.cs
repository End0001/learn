using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyHealthBar : MonoBehaviour
{
    public Image mask;//获取组件
    private float healthBarWidth;//组件的宽度，用于改变血条显示

    public static RubyHealthBar instance { get; private set; }//单例

    public bool hasTask;//是否有任务
    //public bool isCompleteTask;//是否完成任务
    public int fixedNum;//修理机器人的数量

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        healthBarWidth=mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //设置当前血条UI显示值
    public void SetValue(float fillPercent)
    {
        //设置减少的方向、显示的宽度（总宽度*比例）
        mask.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,healthBarWidth*fillPercent );
    }
}
