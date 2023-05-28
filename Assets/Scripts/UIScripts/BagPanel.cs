using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using UnityEngine.UI;

public class BagPanel : VBaseUIForm
{
    /// <summary>
    /// 显示访问过的NPC的根对象
    /// </summary>
    public GameObject characterScrollView;
    /// <summary>
    /// 显示访问过的NPC的Content面板
    /// </summary>
    public GameObject characterArea;
    /// <summary>
    /// 显示与某个NPC交流详细内容的面板
    /// </summary>
    public GameObject detailsPanel;
    /// <summary>
    /// 其上挂在Text Component
    /// </summary>
    public GameObject detailsArea;
    /// <summary>
    /// BagPanel中右侧需求显示区域，InputElem添加到其中
    /// </summary>
    public GameObject inputArea;
    /// <summary>
    /// InputArea中的元素
    /// </summary>
    public GameObject inputElem;
    /// <summary>
    /// CharacterArea中的元素
    /// </summary>
    public GameObject characterElem;
    /// <summary>
    /// 维护一个stinrg类型的List，用于更新回顾面板中角色的模型层
    /// </summary>
    private List<string> alreadyExist;
    /// <summary>
    /// 保存当前显示详细对话面板中的NPC的name，有刷新时使用
    /// </summary>
    private string currentDetailsNPCName;
    /// <summary>
    /// 显示访问过的NPC的面板的宽度，用于计算其包含元素的大小
    /// </summary>
    private float characterAreaWidth = 0;

    // 底下这两个关于提示，找的都是 gameobject， 具体的 component 通过 gameobject 获得。
    private Transform tipsText;
    private Transform tipsBtn;

    private void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;



        //注册退出按钮
        RigisterButtonObjectEvent("ExitBtn",
            p => CloseUIForm()
            );

        //注册添加需求按钮
        RigisterButtonObjectEvent("AddReqBtn",
            p => 
            {
                OpenUIForm("SelectNpcPanel");
                SendMessage("SelectNpcPanelMessage", "", new SelectNpcPanelMessage((npcName) => {
                    string name = 1 + "_" + npcName;
                    if (Global.requirements.ContainsKey(name))
                    {
                        int reqNum = 2;
                        while (Global.requirements.ContainsKey(reqNum + "_" + npcName))
                        {
                            reqNum++;
                        }
                        name = reqNum + "_" + npcName;
                    }

                    Global.AddDetailedLog($"添加了 {npcName} 的第 {name.Split('_')[0]} 条需求");
                    Global.AddSimpleLog($"添加了 {npcName} 的第 {name.Split('_')[0]} 条需求");

                    //模型层
                    GameObject clone = Instantiate(inputElem, inputArea.transform, false);
                    clone.name = name;
                    clone.transform.Find("Text").GetComponent<Text>().text =  $"{Show(npcName)}的第{clone.name.Split('_')[0]}条需求：";
                    clone.SetActive(true);
                    //数据层
                    Global.requirements.Add(name, "");
                    //这里很奇怪，不临时保存一下注册事件就会出问题，我还没搞懂为什么（闭包陷阱）
                    //int tempNum = reqNum;
                    RegisterInputElemBtn(clone);
                    
                }));
  
                /*
                
                int reqNum = 0;
                // 当时咋想的，为啥要这么写，有什么好处吗，我觉得没有
                while (Global.requirements.ContainsKey(reqNum))
                {
                    reqNum++;
                }
                //模型层
                GameObject clone = Instantiate(inputElem, inputArea.transform, false);
                clone.name = reqNum.ToString();
                clone.SetActive(true);
                //数据层
                Global.requirements.Add(reqNum, "");
                //这里很奇怪，不临时保存一下注册事件就会出问题，我还没搞懂为什么（闭包陷阱）
                //int tempNum = reqNum;
                RegisterInputElemBtn(clone);
                
                */
            });
        RigisterButtonObjectEvent("ReturnBtn",
            p =>
            {
                detailsPanel.SetActive(false);
                characterScrollView.SetActive(true);
            });

        // 初始化提示区域，控制灯泡是否闪烁，是否给予提示文字
        InitTipsArea();

        //初始化
        alreadyExist = new List<string>();
        //每次开始第一次载入BagPanel时根据需求字典重新加载，不然不显示。
        RefreshReq();
    }

    private void InitTipsArea()
    {
        Transform TipsArea = transform.Find("TipsArea");
        tipsText = TipsArea.Find("Text");
        tipsBtn = TipsArea.Find("TipsBtn");

        // 注册提示按钮
        RigisterButtonObjectEvent("TipsBtn",
            p =>
            {
                OpenUIForm("TipsPanel");
                Global.AddDetailedLog("打开了提示面板");
                if (Global.hasOpenTipsPanel == false)
                { 
                    Global.hasOpenTipsPanel = true;
                    tipsText.gameObject.SetActive(false);
                    tipsBtn.gameObject.GetComponent<Animator>().SetBool("hasOpen", true);
                }
            });

    }


    // 给需求面板中，Inputfield 和 Button 的组合体中的Button注册事件，InputFiled的事件也来这里注册
    private void RegisterInputElemBtn(GameObject clone)
    {
        // 在结束编辑的时候将内容存到对应的数据结构中（感觉这里好像有更好的写法，数据层和视图层可以统一）
        clone.transform.Find("InputField").GetComponent<InputField>().onEndEdit.AddListener(content =>
        {
            if (!Global.requirements[clone.name].Equals(content))
            {
                Global.requirements[clone.name] = content;
                Global.AddDetailedLog($"编辑了 {clone.name.Split('_')[1]} 的第 {clone.name.Split('_')[0]} 条需求 编辑后内容为 {content}");
                Global.AddSimpleLog($"编辑了 {clone.name.Split('_')[1]} 的第 {clone.name.Split('_')[0]} 条需求");
            }
        });
        //注册“删除”按钮事件
        clone.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            OpenUIForm("ConfirmPanel");
            string[] confirmContent = { "删除需求", $"确定要删除这条需求吗？\n【{Global.requirements[clone.name]}】" };
            SendMessage("ConfirmContent", "", new ConfirmPanelMessage(confirmContent,
                ()=>
                {
                    //print(clone.name);
                    //print(cloneFind("InputField/Text"));
                    //数据层
                    Global.requirements.Remove(clone.name);
                    //模型层
                    Destroy(clone);
                    Global.AddDetailedLog($"删除了 {clone.name.Split('_')[1]} 的第 {clone.name.Split('_')[0]} 条需求");
                    Global.AddSimpleLog($"删除了 {clone.name.Split('_')[1]} 的第 {clone.name.Split('_')[0]} 条需求");
                }));
        });
    }
    private void OnEnable()
    {
        //characterArea.GetComponent<GridLayoutGroup>().cellSize
        RefreshCharacter();
        //及时更新需求面板中的内容（在没有关闭详细面板的情况下）
        ShowOrRefreshDetails(currentDetailsNPCName);

        // 如果已经打开过了，就不闪烁，也不显示文字了 （关于Tips的灯泡）
        if (Global.hasOpenTipsPanel)
        {
            tipsText.gameObject.SetActive(false);
            tipsBtn.gameObject.GetComponent<Animator>().SetBool("hasOpen", true);
        }
    }

    public override void Display()
    {
        base.Display();
        RectTransform parentRect = characterScrollView.GetComponent<RectTransform>();
        if (characterAreaWidth != parentRect.rect.width)
        {
            characterAreaWidth = parentRect.rect.width;
            //print(characterAreaWidth);
            GridLayoutGroup grid = characterArea.GetComponent<GridLayoutGroup>();
            //根据面板的宽度，动态设置GridLayout中元素的大小以及其Padding和Spacing
            //空当区域占元素宽度的百分比
            float ratio = 1f / 4;
            //print(ratio);
            float size = parentRect.rect.width / (grid.constraintCount + ratio * (grid.constraintCount + 1));
            //print(size);
            grid.cellSize = new Vector2(size, size);
            grid.padding.left = (int)(size * ratio);
            grid.padding.right = (int)(size * ratio);
            grid.spacing = new Vector2(size * ratio, size * ratio);
        }
    }

    /// <summary>
    /// 刷新右侧需求面板中的需求，因为其添加与删除是原子的（数据层的添加和模型层的添加都完成了），所以只在Awake时调用一次。
    /// </summary>
    private void RefreshReq()
    {
        //遍历需求字典
        foreach (KeyValuePair<string, string> kvp in Global.requirements)
        {
            GameObject clone = Instantiate(inputElem, inputArea.transform, false);
            clone.name = kvp.Key.ToString();
            clone.GetComponentInChildren<InputField>().text = kvp.Value;
            clone.transform.Find("Text").GetComponent<Text>().text = $"{Show(clone.name.Split('_')[1])}的第{clone.name.Split('_')[0]}条需求";
            RegisterInputElemBtn(clone);
            clone.SetActive(true);
        }
    }

    /// <summary>
    /// 刷新左侧回顾面板中的角色列表，由于与NPC对话后只对其数据层进行了更新，故每次打开BagPanel时需要对模型层进行更新。
    /// </summary>
    private void RefreshCharacter()
    {
        //由于NPC一旦添加就不会消失，所以先比较字典中元素的数量和面板中子物体的数量
        //因为里面有一个隐藏的template 所以 -1
        if (characterArea.transform.childCount - 1 < Global.dialogCollection.Count)
        {
            //遍历字典
            foreach (string npcName in Global.dialogCollection.Keys)
            {
                //如果存在于需求字典中但不存在于该List中，说明是模型层中未及时更新的内容，需要进行更新
                if (!alreadyExist.Contains(npcName))
                {
                    GameObject clone = Instantiate(characterElem, characterArea.transform, false);
                    clone.name = npcName;
                    clone.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("avator/" + npcName);
                    clone.transform.Find("Text").GetComponent<Text>().text = Show(npcName);
                    //为其注册点击事件
                    clone.AddComponent<Button>().onClick.AddListener(() =>
                    {
                        Global.AddDetailedLog($"回顾了与 {npcName} 的对话");
                        Global.AddSimpleLog($"回顾了与 {npcName} 的对话");
                        characterScrollView.SetActive(false);
                        //根据npc名字设置显示的内容
                        currentDetailsNPCName = clone.name;
                        ShowOrRefreshDetails(clone.name);
                        detailsPanel.SetActive(true);
                    });
                    clone.SetActive(true);
                    alreadyExist.Add(npcName);
                }
            }
        }
    }

    private void ShowOrRefreshDetails(string npcName)
    {
        if (string.IsNullOrEmpty(currentDetailsNPCName))
        {
            return;
        }
        Text detailsText = detailsArea.GetComponent<Text>();
        //清空内容
        detailsText.text = "";
        foreach (string str in Global.dialogCollection[npcName])
        {
            //print(str);
            //将对话文件中的'A'替换为玩家，将对话文件中的'B'替换为NPC姓名
            detailsText.text += str.Replace("A\r", $"<b>{GameMgr.GetInstance().playerName}：</b>\r").Replace("B\r", $"<b>{Show(npcName)}：</b>\r");
            detailsText.text += "\n\n";
        }
    }

}
