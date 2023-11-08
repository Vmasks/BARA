using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using UnityEngine.UI;

struct ResolutionValue 
{
    public int width;
    public int height;
    public ResolutionValue(int w, int h)
    {
        width = w;
        height = h;
    }
}


public class SettingPanel : BaseUIForm
{
    public GameObject fullScreenToggle;
    public GameObject resolutionDropdown;
    public GameObject confirmBtn;
    public GameObject closeBtn;
    private bool isFullScreen = false;
    private int currentResolutionIndex = 0;
    private ResolutionValue[] rv =
        {
            new ResolutionValue(1600,900),
            new ResolutionValue(1920,1080),
            new ResolutionValue(1600,1024),
            new ResolutionValue(1280,1024),
            new ResolutionValue(1280,960),
            new ResolutionValue(2560, 1440)
            
            
        }; 
    private void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        fullScreenToggle.GetComponent<Toggle>().onValueChanged.AddListener(value => { isFullScreen = value; });
        resolutionDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(index => { currentResolutionIndex = index; });
        confirmBtn.GetComponent<Button>().onClick.AddListener(() => 
        {
            Screen.SetResolution(rv[currentResolutionIndex].width, rv[currentResolutionIndex].height, isFullScreen);
        });
        closeBtn.GetComponent<Button>().onClick.AddListener(() => { CloseUIForm(); });
    }
}
