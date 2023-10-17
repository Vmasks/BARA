using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;

public class StartProject : MonoBehaviour
{
    void Start()
    {
        UIManager.GetInstance().ShowUIForms("StartPage");
        // 创建游戏管理实例
        DontDestroyOnLoad(GameMgr.GetInstance());
    }

}
