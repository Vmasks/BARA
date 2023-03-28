using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SUIFW;

public class DialogSystem : MonoBehaviour
{
    //显示文字内容的UI组件
    public Text textLabel;
    public Text NPCName;
    public Image faceImage;
    private int index = 0;
    //存储所有语句
    List<string> textList = new List<string>();
    //存储当前对话文件
    private TextAsset textFile;
    //是否输出完一句话
    private bool textFinished;
    private Sprite avator1, avator2;
    private float textSpeed = 0.05f; 
    
    private void Awake()
    {

    }
    private void OnEnable()
    {
        avator1 = Resources.Load<Sprite>("avator/" + GameMgr.GetInstance().mainCharacter);
        avator2 = Resources.Load<Sprite>("avator/" + GameMgr.GetInstance().mainCharacter);
        if (Global.isTalk)
        {   
            textFile = (TextAsset)Resources.Load("Dialog/" + Global.currentTalkNPC.name + '_' + Global.currentTalkNPC.GetComponent<NPCMood>().GetCurrentMood());
        }
        else
        {
            textFile = (TextAsset)Resources.Load("Thinking/" + Global.currentTalkNPC.name + '_' + Global.currentTalkNPC.GetComponent<NPCMood>().GetCurrentMood());
        }
        
        GetTextFromFile(textFile);
  
        textFinished = true;
        //textLabel.text = textList[index++];
        StartCoroutine(SetTextUI());
  
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextPara();
        }

    }

    public void NextPara()
    {
        //恢复文字出现速度
        textSpeed = 0.05f;
        if (textFinished)
        {
            //对话结束
            if (index == textList.Count)
            {
                //gameObject.SetActive(false);
                //关闭对话框（这个写法不好，应当改进）
                //GetComponent<DialogPanel>().CloseDialogPanel();
                //改变NPC状态并记录状态
                if (Global.isTalk)
                {
                    //改变情绪
                    Global.currentTalkNPC.GetComponent<NPCMood>().ChangeMood();
                    //记录情绪
                    if (Global.npcMood.ContainsKey(Global.currentTalkNPC.name))
                    {
                        Global.npcMood[Global.currentTalkNPC.name] = Global.currentTalkNPC.GetComponent<NPCMood>().GetCurrentMood();
                    }
                    else
                    {
                        Global.npcMood.Add(Global.currentTalkNPC.name, Global.currentTalkNPC.GetComponent<NPCMood>().GetCurrentMood());
                    }
                }
                index = 0;
                return;
            }
            //textLabel.text = textList[index++];
            StartCoroutine(SetTextUI());
        }
        else
        {
            textSpeed = 0f;
        }
    }

 
    public void test()
    {
        List<string> tmpList = Global.dialogCollection["Alex"];
        foreach (string str in tmpList)
        {
            print(str);
        }
    }


    private void RecordDialog(TextAsset file)
    {
        string dialogStr = file.text;
        if (!file) return;
        if (!Global.dialogCollection.ContainsKey(Global.currentTalkNPC.name))
        {
            Global.dialogCollection.Add(Global.currentTalkNPC.name, new List<string>());
        }
        List<string> tmpList = Global.dialogCollection[Global.currentTalkNPC.name];
        foreach (string line in tmpList)
        {
            if (line == dialogStr)
            {
                return;
            }
        }
        tmpList.Add(dialogStr);
        Global.dialogCollection[Global.currentTalkNPC.name] = tmpList;
        
    }


    void GetTextFromFile(TextAsset file)
    {
        if (Global.isTalk)
        {
            RecordDialog(file);
        }
        textList.Clear();
        index = 0;
        string[] lineData = file.text.Split('\n');
        foreach (string line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";

        switch(textList[index])
        {
            case "A\r":
                faceImage.sprite = avator1;
                NPCName.text = GameMgr.GetInstance().playerName;
                index++;
                break;
            case "B\r":
                faceImage.sprite = avator2;
                NPCName.text = Global.currentTalkNPC.name;
                index++;
                break; 
        }

        for (int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];
            yield return new WaitForSeconds(textSpeed);
        }
        textFinished = true;
        index++;
    }
}
