using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using SUIFW;

public class DialogPanelBranch : VBaseUIForm
{
    public DialogGraph graph;
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
    private bool canContinue = false;
    private bool sentenceFinished = false;
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
        currentNode = GetStartNode();
        StartCoroutine(TraverseNodes());
    }

    

    // 点击聊天框时触发的方法
    public void NextSentence()
    {
        if (sentenceFinished)
        {
            canContinue = true;
        }
        else
        {
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
