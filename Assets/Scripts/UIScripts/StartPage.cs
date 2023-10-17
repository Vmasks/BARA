using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class StartPage : BaseUIForm
{
    public GameObject startBtn;
    public GameObject loadBtn;
    public GameObject settingBtn;
    public GameObject exitBtn;
    
    private void Awake()
    {
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.Normal;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
        /* 给按钮注册事件 */
        //注册开始游戏事件
        // 先选择场景，再创建角色
        startBtn.GetComponent<Button>().onClick.AddListener(() => OpenUIForm("ScenesSelectPanel"));
        loadBtn.GetComponent<Button>().onClick.AddListener(() => OpenUIForm("LoadPanel"));
        settingBtn.GetComponent<Button>().onClick.AddListener(() => OpenUIForm("SettingPanel"));
        exitBtn.GetComponent<Button>().onClick.AddListener(() => Application.Quit());

    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startBtn);
    }
}
