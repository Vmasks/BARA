using SUIFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenesSelectPanel : BaseUIForm
{
    // 图书馆场景，健身房场景，自己创建的场景
    public Button libBtn, gymBtn, ucdBtn, MyScenarioBtn;
    private void Awake()
    {
        //锟斤拷锟斤拷锟斤拷锟斤拷锟�
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;
        CurrentUIType.UIForms_Type = UIFormType.Normal;
        //注锟斤拷乇沾锟斤拷诎锟脚�
        RigisterButtonObjectEvent("CloseBtn",
            p => CloseUIForm()
        );
        libBtn.onClick.AddListener(() =>
        {
            GameMgr.GetInstance().sceneName = "Lib";
            OpenUIForm("CreatePlayerPanel");
        });
        gymBtn.onClick.AddListener(() =>
        {
            GameMgr.GetInstance().sceneName = "Gym";
            OpenUIForm("CreatePlayerPanel");
        });
        ucdBtn.onClick.AddListener(() =>
        {
            GameMgr.GetInstance().sceneName = "UCD";
            OpenUIForm("CreatePlayerPanel");
        });
        MyScenarioBtn.onClick.AddListener(() =>
        {
            GameMgr.GetInstance().sceneName = "MyScenario";
            OpenUIForm("CreatePlayerPanel");
        });
    }
}
