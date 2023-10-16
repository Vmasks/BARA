using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ReadGraph : MonoBehaviour
{
    public DialogGraph graph;

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
