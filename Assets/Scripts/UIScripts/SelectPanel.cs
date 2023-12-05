using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectPanel : VBaseUIForm
{
    public GameObject talkBtn;
    public GameObject observeBtn;
    public GameObject cancelBtn;

    private void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;

        //注册交谈按钮事件
        talkBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            //这里暂时两个版本并行，根据场景名做区分
            //UCD场景走状态图，支持分支
            //因为有可能的众包场景任务，所以老版本也要兼容，其他场景暂时不迁移，走老版本
            Global.isTalk = true;
            if (GameMgr.GetInstance().sceneName == "UCD")
            {
                OpenUIForm("DialogPanelBranch");
            }
            else
            {
                OpenUIForm("DialogPanel");
            }
            
        });
        //注册观察按钮事件
        observeBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            Global.isTalk = false;
            OpenUIForm("DialogPanel");
        });
        //注册“取消”按钮事件
        cancelBtn.GetComponent<Button>().onClick.AddListener(() => 
        {
            CloseUIForm();
            Global.allowOpenSelectPanel = false;
            Invoke("SetOpenSelectPanel", 0.5f);
        });
    }
    private void SetOpenSelectPanel()
    {
        Global.allowOpenSelectPanel = true;
    }
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(talkBtn);
    }
    private void Start()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(talkBtn);
    }
}
