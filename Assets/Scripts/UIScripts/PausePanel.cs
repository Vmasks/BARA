using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SUIFW;
using GameSave;

public class PausePanel : VBaseUIForm
{
    public GameObject resumeBtn;
    public GameObject saveBtn;
    public GameObject settingBtn;
    public GameObject exitBtn;
    public GameObject exportBtn;
    public Text talkNumText;
    public Text reNumText;
    public Text test;

    public Text testText;
    public GameObject LogArea;

    private bool isLogAreaActivate = false;

    private void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        RigisterButtonObjectEvent("ResumeBtn",
            p => {
                CloseUIForm();
                Global.AddDetailedLog("继续游戏");
            });
        RigisterButtonObjectEvent("SaveBtn",
            p =>
            {
                OpenUIForm("SavePanel");
            }
            );
        //注册导出需求按钮
        RigisterButtonObjectEvent("ExportBtn",
            p =>
            {
                OpenUIForm("ExportPanel");
            });
        settingBtn.GetComponent<Button>().onClick.AddListener(() => OpenUIForm("SettingPanel"));
        exitBtn.GetComponent<Button>().onClick.AddListener(() => 
        {
            OpenUIForm("ConfirmPanel");
            string[] confirmContent = { "退出游戏", $"确定要退出游戏吗？\n当前游戏进度将保存在【{GameMgr.GetInstance().playerName}的自动存档】中。" };
            SendMessage("ConfirmContent", "", new ConfirmPanelMessage(confirmContent,
                () =>
                {
                    GameSaveMgr.GetInstance().AutoSave();
                    Application.Quit();
                }));
        });
    }
    private void OnEnable()
    {
        talkNumText.text = $"已访问：{Global.dialogCollection.Count}人";
        reNumText.text = $"已写需求：{Global.requirements.Count}条";
        testLog();
        // test.text = Global.branchRecord["Lynette"][0][0];
        
        // foreach (var kvp in Global.branchRecord){
        //     print("Key = " + kvp.Key + "Value = " + kvp.Value + "\r");
        // }

        // print("length:" + Global.branchRecord["Lynette"].Count.ToString());

        // print("length:" + Global.branchRecord["Lynette"][0].Count.ToString());

        // test.text = Global.branchRecord["Lynette"][0][0];
        
    }

    private void testLog()
    {
        testText.text = "";
        foreach(string log in Global.detailedLog)
        {
            testText.text += (log + "\n");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            isLogAreaActivate = !isLogAreaActivate;
            LogArea.SetActive(isLogAreaActivate);
        }
    }
}
