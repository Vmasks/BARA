using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConfirmPanelMessage 
{
    public UnityAction cd;
    public string[] messageStr;
    public GameObject clone;
    public ConfirmPanelMessage(string[] contentList , UnityAction cd)
    {
        messageStr = contentList;
        this.cd = cd;
    }
}
