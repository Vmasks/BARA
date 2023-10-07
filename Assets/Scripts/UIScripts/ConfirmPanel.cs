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
        //��������
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //��������
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
