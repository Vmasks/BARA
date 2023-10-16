using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ReadGraph : MonoBehaviour
{
    public DialogGraph graph;

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

    // Start is called before the first frame update
    void Start()
    {
        print("start!");
        // GetStartNode();
        Chat node = GetStartNode() as Chat;
        foreach (string sentence in node.content)
        {
            print(sentence);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
