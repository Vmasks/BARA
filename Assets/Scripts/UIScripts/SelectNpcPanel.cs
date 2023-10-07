using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using UnityEngine.UI;


public class SelectNpcPanel : VBaseUIForm
{
    [SerializeField]
    private GameObject npcArea, characterElem;

    // 记录已经存在的NPC
    private List<string> alreadyExist;

    private void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
       
        RigisterButtonObjectEvent("ExitBtn",p => {
            CloseUIForm();
        });

        // 初始化
        alreadyExist = new List<string>();

    }
    private void OnEnable()
    {
        // 因为使用了UI框架的缘故，Enable 在第一次调用的时候会执行两次，之后就正常了。
        if (npcArea.transform.childCount < Global.dialogCollection.Count)
        {
            ReceiveMessage("SelectNpcPanelMessage", p => {
                // 这个操作很关键，如果不清空的话，就会一直堆积，因为他提供的Rigster那个函数是每次都会调用的，所以每次都会Send。
                SelectNpcPanelMessage snpm = p.Values as SelectNpcPanelMessage;

                foreach (string npcName in Global.dialogCollection.Keys)
                {
                    if (!alreadyExist.Contains(npcName))
                    {
                        GameObject clone = Instantiate(characterElem, npcArea.transform, false);
                        clone.name = npcName;
                        clone.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("avator/" + npcName);
                        clone.transform.Find("Text").GetComponent<Text>().text = Show(npcName);
                        //为其注册点击事件
                        clone.AddComponent<Button>().onClick.AddListener(() => 
                        {
                            snpm.cd(npcName);
                            CloseUIForm();
                        });
                        alreadyExist.Add(npcName);
                    }

                }
                

                // 这里为什么每次都执行？
                // print("haha");
            });


        }


    }
}
