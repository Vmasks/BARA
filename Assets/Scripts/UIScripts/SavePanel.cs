using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SUIFW;
using GameSave;
using System.IO;

public class SavePanel : BaseUIForm
{
    public InputField inputField;
    public Image playerAvator;
    public Text PlayerName;
    public Text TalkNum;
    public Text reNum;
    public Text gameTime;
    public Text systemTime;
    public Text successTip;
    private string storeName;
    //private string[] storeNameList;

    private void CloseTip()
    {
        successTip.gameObject.SetActive(false);
        CloseUIForm();
    }

    private void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        RigisterButtonObjectEvent("SaveBtn",
            p => 
            {
                if (GameSaveMgr.GetInstance().Save(storeName))
                {

                    successTip.gameObject.SetActive(true);
                    Invoke("CloseTip", 2.0f);
                }
            }
            );
        RigisterButtonObjectEvent("CloseBtn",
            p => 
            {
                CloseUIForm();
            }
            );
        inputField.GetComponent<InputField>().onEndEdit.AddListener(content => { storeName = content; });
        playerAvator.sprite = Resources.Load<Sprite>("Avator/" + GameMgr.GetInstance().mainCharacter);
        PlayerName.text = GameMgr.GetInstance().playerName;
    }
    private void OnEnable()
    {
        inputField.text = Global.storeName;
        storeName = Global.storeName;
        TalkNum.text = $"已访问：{Global.dialogCollection.Count}人";
        reNum.text = $"已写需求：{Global.requirements.Count}条";
        //storeNameList = GameSaveMgr.GetInstance().GetFileNameList();
}
    private void Update()
    {
        systemTime.text = System.DateTime.Now.ToString();
        gameTime.text = "游戏时长：" + GameSaveMgr.ConvertTimeStr(Global.gameTime);
    }
    
}
