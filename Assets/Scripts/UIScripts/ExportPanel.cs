using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;
using GameSave;

public class ExportPanel : VBaseUIForm
{
    public Button exitBtn;
    public InputField inputField;
    public Button exportBtn;
    public Text tips;
    //学号
    private string stuNum;
    private void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        //注册退出按钮事件
        exitBtn.onClick.AddListener(() => { CloseUIForm(); });
        inputField.onEndEdit.AddListener(content => 
        {
            stuNum = content;
        });
        exportBtn.onClick.AddListener(() => 
        {
            if (string.IsNullOrEmpty(stuNum))
            {
        
                tips.color = new Color(209f/255, 27f/255, 46f/255);
                tips.text = "导出未完成，请输入学号";
            }
            //导出
            else
            {
                //string exportStr = "";
                //exportStr += $"学号：{stuNum}\n";
                //exportStr += $"角色名：{Global.name}\n";
                //exportStr += $"游戏时长：{GameSaveMgr.ConvertTimeStr(Global.gameTime)}\n";
                //exportStr += $"导出时间：{DateTime.Now}\n";
                //exportStr += $"已访问：{Global.dialogCollection.Count}人\n";
                //exportStr += $"已写需求：{Global.requirements.Count}条\n";
                //int num = 1;
                //foreach (string req in Global.requirements.Values)
                //{
                //    exportStr += num++ +  ".\n" + req + "\n\n";
                //}
                GameSaveMgr.GetInstance().ExportResult(stuNum, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                //File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + $"/{stuNum}", GameSaveMgr.Encrypt(exportStr), Encoding.UTF8);
                //File.WriteAllText(Environment.CurrentDirectory + $"/{stuNum}", GameSaveMgr.Encrypt(exportStr), Encoding.UTF8);
                tips.color = new Color(50f / 255, 156f / 255, 50f / 255);
                tips.text = "导出成功";
                Global.AddDetailedLog($"导出成果 {stuNum}");
                Invoke("closePanel", 2.2f);
                //print(exportStr);
            }
        });
    }
    private void OnEnable()
    {
        tips.text = "";
    }
    private void closePanel()
    {
        CloseUIForm();
    }

}
