using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using UnityEngine.UI;


public class ConfirmPanel : VBaseUIForm
{
    public GameObject title;
    public GameObject content;
    public GameObject confirmBtn;
    public GameObject cancelBtn;
    private void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;

        ReceiveMessage("ConfirmContent", p => 
        {
            confirmBtn.GetComponent<Button>().onClick.RemoveAllListeners();
            ConfirmPanelMessage cm = p.Values as ConfirmPanelMessage;
            title.GetComponent<Text>().text = cm.messageStr[0];
            content.GetComponent<Text>().text = cm.messageStr[1];
            cm.cd += () => { CloseUIForm(); };
            confirmBtn.GetComponent<Button>().onClick.AddListener(cm.cd);
            

        });
        cancelBtn.GetComponent<Button>().onClick.AddListener(() => { CloseUIForm(); });
    }
}
