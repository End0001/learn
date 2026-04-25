using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("Interal Clock")]

    //序列化字段，让其在全代码可用
    [SerializeField]
    GameTimeStamp timeStamp;
    public float timeScale = 1.0f;

    [Header("Day and Night cycle")]
    //光源位置
    public Transform sunTransform;

    //观察者模式
    List<ITimeTrack> listeners=new List<ITimeTrack>();

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

    // Start is called before the first frame update
    void Start()
    {
        //时间戳
        timeStamp = new GameTimeStamp(0, GameTimeStamp.Season.Spring, 1, 6, 0);

        StartCoroutine(TimeUpdate());
    }


    IEnumerator TimeUpdate()
    {
        while (true)
        {
            Tick();
            yield return new WaitForSeconds(1/timeScale);
            
        }
        
    }
    public void Tick()
    {
        timeStamp.UpdateClock();

        foreach(ITimeTrack listener in listeners)
        {
            listener.ClockUpdate(timeStamp); 
        }

        UpdateSunMovement();
    }

    //处理光源（太阳）旋转
    void UpdateSunMovement()
    {
        //计算光源旋转（实现一天的太阳效果）
        int timeInMinutes = GameTimeStamp.HoursToMinutes(timeStamp.hour) + timeStamp.minute;

        float sunAngle = 0.25f * timeInMinutes - 90;

        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);

    }

    public GameTimeStamp GetGameTimeStamp()
    {
        //克隆一个时间对象，作为参考
        return new GameTimeStamp(timeStamp);
    }

    //将对象加入接收消息队列
    public void RegisterTracker(ITimeTrack listener)
    {
        listeners.Add(listener);
    }
    //将对象移除接收消息队列
    public void UnregisterTracker(ITimeTrack listener)
    {
        listeners.Remove(listener);
    }
}
