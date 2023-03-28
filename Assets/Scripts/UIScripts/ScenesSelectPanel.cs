using SUIFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenesSelectPanel : BaseUIForm
{
    public Button libBtn, gymBtn;
    private void Awake()
    {
        //窗体的性质
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;
        CurrentUIType.UIForms_Type = UIFormType.Normal;
        //注册关闭窗口按钮
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
    }
}
