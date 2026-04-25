using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialog : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dialogBox;//对话框
    public TextMeshProUGUI newDialog;//新的文本内容

    public GameObject startTaskDGBox;//开始任务文本框
    public GameObject completeTaskDGBox;//完成任务文本框

    public AudioSource audioSource;//音频组件
    public AudioClip completeTaskClip;//完成任务音频 

    private bool CanPlayStart = true;//是否可播放
    private bool CanPlayComplete = true;//是否可播放
    
    void Start()
    {
        dialogBox.SetActive(false);
        startTaskDGBox.SetActive(false);
        completeTaskDGBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (RubyHealthBar.instance.fixedNum >= 5 && CanPlayComplete)
        {
            audioSource.PlayOneShot(completeTaskClip);//播放任务完成音效
            CanPlayComplete = false;//设置成不可播放，只播放一次就行
            ShowTaskTip(completeTaskDGBox);//完成任务显示
            Invoke("CloseTaskTip", 1f);
        }

    }

    public void DisplayDialog(bool value)
    {
        dialogBox.SetActive (value);
        RubyHealthBar.instance.hasTask = true;
        if(CanPlayStart)//只显示一次开始任务
        {
            ShowTaskTip(startTaskDGBox);//开始任务显示
            Invoke("CloseTaskTip", 1f);
            CanPlayStart = false;
        }
        if (RubyHealthBar.instance.fixedNum>=5)
        {
            //完成任务，修改对话框内容           
            newDialog.text = "心地善良的小姐，谢谢你，你实在是太棒了！";
            
        }
    }

    public void ShowTaskTip(GameObject t)//显示任务提示
    {
        t.SetActive(true);
    }

    public void CloseTaskTip()//关闭任务提示
    {
        startTaskDGBox.SetActive(false);
        completeTaskDGBox.SetActive(false);
    }
}
