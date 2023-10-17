using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using UnityEngine.UI;
using System;

public class DialogPanel : VBaseUIForm
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
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        gameObject.AddComponent<Button>().onClick.AddListener(NextPara);
    }
    override public void Display()
    {
        base.Display();
        avator1 = Resources.Load<Sprite>("avator/" + GameMgr.GetInstance().mainCharacter);
        avator2 = Resources.Load<Sprite>("avator/" + GameMgr.GetInstance().sceneName + "/" + Global.currentTalkNPC.name);
        if (Global.isTalk)
        {
            print(GameMgr.GetInstance().sceneName + "/" + "Dialog/" + Global.currentTalkNPC.name + '/' + Global.currentTalkNPC.GetComponent<NPCMoodTest>().currentState);
            textFile = (TextAsset)Resources.Load(GameMgr.GetInstance().sceneName + "/" + "Dialog/" + Global.currentTalkNPC.name + '/' + Global.currentTalkNPC.GetComponent<NPCMoodTest>().currentState);
        }
        else
        {
            textFile = (TextAsset)Resources.Load(GameMgr.GetInstance().sceneName + "/" + "Thinking/" + Global.currentTalkNPC.name + '/' + Global.currentTalkNPC.GetComponent<NPCMoodTest>().currentState);
        }

        if (textFile != null)
        {
            GetTextFromFile(textFile);
        }
        else
        {
            textList.Clear();
            textList.Add("B");
            textList.Add("...");
        }
        textFinished = true;
        // print("haha");
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
                // GetComponent<DialogPanel>().CloseDialogPanel();
                // 直接关闭不就好啦 23-3-12
                CloseUIForm();
                //改变NPC状态并记录状态
                // 改变之前要记录一下（为啥要记录一下？）
                if (!Global.hasCollectedMood.ContainsKey(Global.currentTalkNPC.name))
                {
                    Global.hasCollectedMood.Add(Global.currentTalkNPC.name, new HashSet<string>());
                }
                if (Global.isTalk)
                {

                    print ($"对话 {Global.currentTalkNPC.name}");
                    Global.AddDetailedLog($"对话 {Global.currentTalkNPC.name} {Global.currentTalkNPC.GetComponent<NPCMoodTest>().currentState}");
                    string currentMood = Global.currentTalkNPC.GetComponent<NPCMoodTest>().currentState;
                    if (!Global.hasCollectedMood[Global.currentTalkNPC.name].Contains(currentMood))
                    {
                        Global.hasCollectedMood[Global.currentTalkNPC.name].Add(currentMood);
                    }
                    //foreach (string key in Global.hasCollectedMood.Keys)
                    //{
                    //    print(key + ": ");
                    //    foreach(string mood in Global.hasCollectedMood[key])
                    //    {
                    //        print(mood + " ");
                    //    }
                    //    print("\n");
                    //}
                    
                    //改变状态
                    Global.currentTalkNPC.GetComponent<NPCMoodTest>().ChangeState("Talk");
                    print("DialogPanel 改变了状态");

                }
                // 观察
                else
                {
                    Global.currentTalkNPC.GetComponent<NPCMoodTest>().ChangeState("Observe");
                    print("DialogPanel 改变了状态");
                    Global.AddDetailedLog($"观察 {Global.currentTalkNPC.name} {Global.currentTalkNPC.GetComponent<NPCMoodTest>().currentState}");
                }
                //记录状态
                if (Global.npcMood.ContainsKey(Global.currentTalkNPC.name))
                {
                    Global.npcMood[Global.currentTalkNPC.name] = Global.currentTalkNPC.GetComponent<NPCMoodTest>().currentState;
                }
                else
                {
                    Global.npcMood.Add(Global.currentTalkNPC.name, Global.currentTalkNPC.GetComponent<NPCMoodTest>().currentState);
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
        string[] lineData = file.text.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        foreach (string line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";

        switch (textList[index])
        {
            case "A\r":
                faceImage.sprite = avator1;
                NPCName.text = GameMgr.GetInstance().playerName;
                index++;
                break;
            case "B\r":
                faceImage.sprite = avator2;
                NPCName.text = Show(Global.currentTalkNPC.name);
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
    //public void CloseDialogPanel()
    //{
    //    CloseUIForm();

    //}

}
