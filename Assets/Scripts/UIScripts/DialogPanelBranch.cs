using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using SUIFW;

public class DialogPanelBranch : BaseUIForm
{
    public DialogGraph graph;
    private GameObject dialogPanel;
    private GameObject branchPanel;
    public Image sp;
    public Text playerName;
    public Text dialogContent;
    public Sprite sp1, sp2;
    public string name1, name2;
    private float textSpeed = 0.05f;
    private bool flag = false;
    private bool sentenceFinished = false;
    // �ҵ���һ���ڵ㣬��û�����Ϊ�յĽڵ�
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
        //��������
        CurrentUIType.UIForms_Type = UIFormType.PopUp; //��������
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        dialogPanel = transform.Find("DialogPanel").gameObject;
        branchPanel = transform.Find("BranchPanel").gameObject;
        gameObject.AddComponent<Button>().onClick.AddListener(NextSentence);
        //sp.sprite = sp2;
        //playerName.text = name2;
    }
    public override void Display()
    {
        base.Display();
        Node firstNode = GetStartNode();
        Node node = firstNode;
        while (node != null)
        {
            if (node is Chat)
            {
                dialogPanel.SetActive(true);
                branchPanel.SetActive(false);
                Chat chatNode = node as Chat;
                //print("chat");
                StartCoroutine(ShowText(chatNode.content));
                node = chatNode.MoveNext();
            }
            else if (node is Branch)
            {
                dialogPanel.SetActive(false);
                branchPanel.SetActive(true);
                Branch branchNode = node as Branch;
                // print("branch");
                // print($"pos: {branchNode.posDesc}");
                // print($"neg: {branchNode.negDesc}");
                node = branchNode.MoveNext(false);
            }
        }
        CloseUIForm();
        //print("end");
    }

    // ��������ʱ�����ķ���
    public void NextSentence()
    {
        if (sentenceFinished)
        {
            flag = true;
        }
        else
        {
            textSpeed = 0f;
        }
    }

    IEnumerator ShowText(List<string> content)
    {
        foreach (string sentence in content)
        {
            flag = false;
            //print(sentence);
            // �����ж�һ�����ַ���A����B���� sp �� playerName ��ֵ
            char firstChar = sentence[0];
            switch (firstChar)
            {
                case 'A':
                    sp.sprite = sp1;
                    playerName.text = name1;
                    break;
                case 'B':
                    sp.sprite = sp2;
                    playerName.text = name1;
                    break;
            }
            foreach (char ch in sentence.Substring(1))
            {
                dialogContent.text += ch;
                yield return new WaitForSeconds(textSpeed);
            }
            while (!flag)
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
