using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SUIFW;
using GameSave;
using UnityEngine.SceneManagement;

public class LoadPanel : BaseUIForm
{
    public Text storeNum;
    public GameObject loadElem;
    public GameObject contentArea;
    private void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        RigisterButtonObjectEvent("ExitBtn",
            p =>
            {
                CloseUIForm();
            }
            );
        string[] fileNameList = GameSaveMgr.GetInstance().GetFileNameList();
        storeNum.text = $"共{fileNameList.Length}个存档";
        foreach(string fileName in fileNameList)
        {
            GameObject clone = Instantiate(loadElem, contentArea.transform, false);
            //没什么用只是觉得应该有个名字
            clone.name = fileName;
            //根据存档名加载存档数据
            GameSaveData gsd = GameSaveMgr.GetInstance().Load(fileName);
            if (gsd != null)
            {
                //设置LoadElem中的内容，并为其注册载入游戏的点击事件
                clone.transform.Find("Avator").GetComponent<Image>().sprite = Resources.Load<Sprite>("Avator/" + gsd.player);
                clone.transform.Find("StoreName").GetComponent<Text>().text = gsd.storeName;
                clone.transform.Find("GameCoreData").GetComponent<Text>().text = $"已访问：{gsd.allDialogue.Count}人   已写需求：{gsd.requirements.Count}条";
                clone.transform.Find("GameTime").GetComponent<Text>().text = "游戏时长：" + GameSaveMgr.ConvertTimeStr(gsd.gameTime);
                clone.transform.Find("SaveTime").GetComponent<Text>().text = gsd.saveTime.ToString();
                string sceneNameStr = "未知场景";
                if (gsd.globalSceneName == "Gym")
                {
                    sceneNameStr = "体育馆";
                }
                else if (gsd.globalSceneName == "Lib")
                {
                    sceneNameStr = "图书馆";
                }
                clone.transform.Find("SceneName").GetComponent<Text>().text = sceneNameStr;
                clone.AddComponent<Button>().onClick.AddListener(() => 
                {
                    //将存档数据赋给当前游戏环境
                    GameMgr.GetInstance().playerName = gsd.name;
                    GameMgr.GetInstance().mainCharacter = gsd.player;
                    // 当前大场景是什么 Lib, Gym
                    GameMgr.GetInstance().sceneName = gsd.globalSceneName;
                    Global.gameTime = gsd.gameTime;
                    Global.dialogCollection = gsd.allDialogue;
                    Global.requirements = gsd.requirements;
                    Global.npcMood = gsd.npcMood;
                    //载入游戏时需要这个名字，赋给Global.storeName，用于存档时自动设置相同的存档名称
                    Global.storeName = fileName;

                   
                    Global.hasOpenTipsPanel = gsd.hasOpenTipsPanel;
                    Global.hasCollectedMood = gsd.hasCollectedMood;

                    // log
                    Global.detailedLog = gsd.detailedLog;
                    Global.simpleLog = gsd.simpleLog;

                    //加入读取存档
                    Global.AddDetailedLog($"读取存档 {gsd.storeName}");

                    //关闭两个UI
                    UIManager.GetInstance().CloseUIForms("LoadPanel");
                    UIManager.GetInstance().CloseUIForms("StartPage");
                    //加载场景
                    SceneManager.LoadScene(gsd.sceneName);
                    //打开主面板
                    UIManager.GetInstance().ShowUIForms("MainPanel");
                    //根据选择的角色名加载玩家角色
                    GameObject player = Resources.Load<GameObject>("Player/" + GameMgr.GetInstance().mainCharacter);
                    Instantiate(player, new Vector3(gsd.posX, gsd.posY, 0), new Quaternion());
                });
            }

        }
    }
}

/*
         gd = GameSaveMgr.GetInstance().Load(gameObject.name);
        avator.sprite = Resources.Load<Sprite>("Avator/" + gd.player);
        storeName.text = gd.storeName;
        gameCoreData.text = $"已访问：{gd.allDialogue.Count}人   已写需求：{gd.requirements.Count}条";
        gameTime.text = "游戏时长：" + GameSaveMgr.ConvertTimeStr(gd.gameTime);
        saveTime.text = gd.saveTime.ToString();
 */