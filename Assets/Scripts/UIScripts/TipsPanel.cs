using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using UnityEngine.UI;

public class TipsPanel : VBaseUIForm
{
    private void Awake()
    {
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        RigisterButtonObjectEvent("ExitBtn",
            p => CloseUIForm()
        );
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
