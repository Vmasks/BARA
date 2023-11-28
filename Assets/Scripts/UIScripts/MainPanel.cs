using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;

public class MainPanel : VBaseUIForm
{
    private void Awake()
    {
        //窗体的性质
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;
        CurrentUIType.UIForms_Type = UIFormType.Fixed;
        RigisterButtonObjectEvent("WriteBtn",
            p =>
            {
                OpenUIForm("BagPanel");
                Global.AddDetailedLog("打开了需求面板");
            }
            );
        RigisterButtonObjectEvent("PauseBtn",
            p => 
            {
                OpenUIForm("PausePanel");
                Global.AddDetailedLog("打开了暂停面板");
            });
        RigisterButtonObjectEvent("tmpBtn",
            p =>
            {
                OpenUIForm("DialogPanelBranch");
            });
    }
}
