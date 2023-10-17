using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoodTest : MonoBehaviour
{
    // 向外暴露一个CurrentState 用于加载对应的对话或者观察内容
    public string currentState;
    // 状态机
    private List<NPCState> fsm;
    
    // 初始化状态机，读取相应的状态文本，填充状态List
    
    // 每次切换场景都会导致重新加载状态机
    private void Awake()
    {
        //读一行一行的，然后填字典
        fsm = new List<NPCState>();
        print($"{GameMgr.GetInstance().sceneName}/StateMachine/{gameObject.name}");
        TextAsset ta = (TextAsset)Resources.Load($"{GameMgr.GetInstance().sceneName}/StateMachine/" + gameObject.name);
        // print(ta.text);
        string[] taLines = ta.text.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        // 如果不是只有一个 Init的话，就处理，
        if (!(taLines[0].Split(' ').Length == 1))
        {
            foreach(string stateLine in taLines)
            {
                string[] stateElems = stateLine.Split(' ');
                fsm.Add(new NPCState(stateElems[0], stateElems[1], stateElems[2]));
            }
            currentState = fsm[0].currentState;
            CheckState();
        }
        else
        {
            currentState = taLines[0];
        }
        // print(this.gameObject.name);
    }

    // 每次状态转移完后调用，用于检测新状态是否有固定时间跳转到别的状态的操作，有的话在这里就 StartCoroutine
    private void CheckState()
    {
        // print("调用CheckState");
        // 遍历状态机
        foreach(NPCState state in fsm)
        {
            if (currentState.Equals(state.currentState))
            {
                float t;
                if (float.TryParse(state.condition, out t))
                {
                    // print("启动协程");
                    StartCoroutine(ChangeStateByTime(t, state.targetState));
                }
                
            }
        }
    }

    public void ChangeState(string condition)
    {
        // print("调用ChangeState");
        foreach (NPCState state in fsm)
        {
            if (currentState.Equals(state.currentState))
            {
                if (condition.Equals(state.condition))
                {
                    // 这里结束全部是不妥的
                    StopAllCoroutines();
                    currentState = state.targetState;
                    if (currentState == "end")
                    {
                        SetState("init");
                    }
                    else
                    {
                        CheckState();
                    }
                    break;
                }
            }
        }
    }

    private IEnumerator ChangeStateByTime(float sec, string state)
    {
        yield return new WaitForSeconds(sec);
        currentState = state;
        if (currentState == "end")
        {
            SetState("init");
        }
        else
        {
            CheckState();
        }
    }

    
    public void SetState(string state)
    {
        currentState = state;
        CheckState();
    }


}
