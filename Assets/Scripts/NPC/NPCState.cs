using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCState
{
    public string currentState;
    public string condition;
    public string targetState;
    public NPCState(string currentState, string condition, string targetState)
    {
        this.currentState = currentState;
        this.condition = condition;
        this.targetState = targetState;
    }
}
