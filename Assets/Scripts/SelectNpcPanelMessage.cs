using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectNpcPanelMessage
{
    public UnityAction<string> cd;
    public SelectNpcPanelMessage(UnityAction<string> cd)
    {
        this.cd = cd;
    }
}
