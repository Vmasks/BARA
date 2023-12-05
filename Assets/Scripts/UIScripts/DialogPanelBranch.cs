using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using SUIFW;

public class DialogPanelBranch : VBaseUIForm
{
    private DialogGraph graph;
    private GameObject dialogPanel;
    private GameObject branchPanel;
    public Button posBtn, negBtn;
    public Text posText, negText;
    public Image sp;
    public Text playerName;
    public Text dialogContent;
    public Sprite sp1, sp2;
    public string name1, name2;
    private float textSpeed = 0.07f;
    //控制协程是否可以继续，用于分支选择等待和进行下一句话等待
    private bool canContinue = false;
    private bool sentenceFinished = false;
    //用于指示有没有找到相应的对话图，如果没有，直接点击对话框即可关闭
    private bool isFindGraph = false;
    //当前节点
    private Node currentNode = null;
    // 找到第一个节点，即没有入口为空的节点
    private Node GetStartNode()
    {
        foreach (Node node in graph.nodes)
        {
            NodePort inPort = node.GetInputPort("input");
            if (!inPort.IsConnected)
            {
                return node;
            }
        }
        return null;
    }
    private void Awake()
    {
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.PopUp; //弹出窗体
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        dialogPanel = transform.Find("DialogPanel").gameObject;
        branchPanel = transform.Find("BranchPanel").gameObject;
        gameObject.AddComponent<Button>().onClick.AddListener(NextSentence);
        //sp.sprite = sp2;
        //playerName.text = name2;
        posBtn.onClick.AddListener(() => {
            canContinue = true;
            Branch node = currentNode as Branch;
            currentNode = node.MoveNext(true);
        });
        negBtn.onClick.AddListener(() => {
            canContinue = true;
            Branch node = currentNode as Branch;
            currentNode = node.MoveNext(false);
        });
    }
    public override void Display()
    {
        base.Display();
        sp1 = Resources.Load<Sprite>($"avator/{GameMgr.GetInstance().mainCharacter}");
        sp2 = Resources.Load<Sprite>($"avator/{GameMgr.GetInstance().sceneName}/{Global.currentTalkNPC.name}");
        name1 = GameMgr.GetInstance().playerName;
        name2 = Show(Global.currentTalkNPC.name);
        //根据场景，Dialog/Observe，角色名，状态加载对应的状态图
        //注意是有可能加载不到的，要做处理
        graph = Resources.Load<DialogGraph>($"{GameMgr.GetInstance().sceneName}/Dialog/{Global.currentTalkNPC.name}/{Global.currentTalkNPC.GetComponent<NPCMoodTest>().currentState}");
        print($"{GameMgr.GetInstance().sceneName}/Dialog/{Global.currentTalkNPC.name}/{Global.currentTalkNPC.GetComponent<NPCMoodTest>().currentState}");
        if (!graph)
        {
            print("没有找到对话图");
            dialogContent.text = "...";
            sp.sprite = sp2;
            playerName.text = name2;
            isFindGraph = false;
            //StartCoroutine(ExecuteNextFrame());
            return;
        }
        isFindGraph = true;
        currentNode = GetStartNode();
        StartCoroutine(TraverseNodes());
    }

    // 这里关闭窗口的话需要在下一帧关，不知道为什么，但是先这样做
    private IEnumerator ExecuteNextFrame()
    {
        yield return null;
        CloseUIForm();
    }


    // 点击聊天框时触发的方法
    public void NextSentence()
    {
        // 如果没找到对画图，点击即关闭对话框
        if (!isFindGraph)
        {
            CloseUIForm();
            return;
        }
        if (sentenceFinished)
        {
            canContinue = true;
        }
        else
        {
            //瞬间出完所有对话
            textSpeed = 0f;
        }
    }

    IEnumerator TraverseNodes()
    {
        while (currentNode != null)
        {
            if (currentNode is Chat)
            {
                dialogPanel.SetActive(true);
                branchPanel.SetActive(false);
                Chat chatNode = currentNode as Chat;
                //print("chat");
                yield return StartCoroutine(ShowText(chatNode.content));
                if (chatNode.nextState != null && chatNode.nextState.Length > 0)
                {
                    // 这里暂时实现得比较粗暴，直接设置状态，而不是有一个状态机，根据条件改变状态。
                    Global.currentTalkNPC.GetComponent<NPCMoodTest>().SetState(chatNode.nextState);
                }
                currentNode = chatNode.MoveNext();
            }
            else if (currentNode is Branch)
            {
                dialogPanel.SetActive(false);
                branchPanel.SetActive(true);
                canContinue = false;
                Branch branchNode = currentNode as Branch;
                posText.text = branchNode.posDesc;
                negText.text = branchNode.negDesc;
                // 等待玩家做出选择
                while (!canContinue) {
                    yield return null;
                }                
            }
        }
        CloseUIForm();
    }

    IEnumerator ShowText(List<string> content)
    {
        foreach (string sentence in content)
        {
            canContinue = false;
            sentenceFinished = false;
            //print(sentence);
            // 首先判断一下首字符是A还是B，给 sp 和 playerName 赋值
            char firstChar = sentence[0];
            switch (firstChar)
            {
                case 'A':
                    sp.sprite = sp1;
                    playerName.text = name1;
                    break;
                case 'B':
                    sp.sprite = sp2;
                    playerName.text = name2;
                    break;
            }
            dialogContent.text = "";
            foreach (char ch in sentence.Substring(1))
            {
                dialogContent.text += ch;
                yield return new WaitForSeconds(textSpeed);
            }
            sentenceFinished = true;
            while (!canContinue)
            {
                yield return null;
            }
            dialogContent.text = "";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
