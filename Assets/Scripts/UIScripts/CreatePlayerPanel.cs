using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SUIFW;
using UnityEngine.UI;

public class CreatePlayerPanel : BaseUIForm
{
    public Image target;
    private void Awake()
    {
        //窗体的性质
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;
        CurrentUIType.UIForms_Type = UIFormType.Normal;
        //注册关闭窗口按钮
        RigisterButtonObjectEvent("CloseBtn",
            p => CloseUIForm()
            ); 
        //注册开始游戏按钮
        RigisterButtonObjectEvent("StartBtn",
            p =>
            {
                CloseUIForm();
                
                // 这块写的也不好，和UI没有分离开

                //SceneManager.LoadScene("Gym1");
                //GameObject player = Resources.Load<GameObject>("Player/" + Global.player);
                //Instantiate(player, new Vector3(10f, -8, 0), new Quaternion());
                //OpenUIForm("MainPanel");
                GameMgr.GetInstance().StartGame();
                // 23-3-12 算是找到了一种我认为比较好的方案, 单独设置一个 GameMgr, 负责这些
                // UI面板只负责接受用户输入
                // 在 这个 UI 中 接收了用户自定义名称以及主角名字.
            }
            );
        //为InputField添加监听事件
        transform.Find("InputField").GetComponent<InputField>().onEndEdit.AddListener(content => {
            GameMgr.GetInstance().playerName = content;
        });
        //这里感觉这样写还是不太好，但是目前没想到更好的写法
        //遍历4个子物体
        for (int i = 0; i < transform.Find("Panel/SelectAspectPanel").childCount; i++)
        {
            GameObject player = transform.Find("Panel/SelectAspectPanel").GetChild(i).gameObject;
            player.AddComponent<Button>().onClick.AddListener(() => 
            {
                //切换图片
                target.sprite = player.GetComponent<Image>().sprite;
                //切换动画
                target.GetComponent<Animator>().SetTrigger(player.name);
                // Global.player = player.name;
                GameMgr.GetInstance().mainCharacter = player.name;
            });
        }
    }
}
