using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoodTest : MonoBehaviour
{
    // ���Ⱪ¶һ��CurrentState ���ڼ��ض�Ӧ�ĶԻ����߹۲�����
    public string currentState;
    // ״̬��
    private List<NPCState> fsm;
    
    // ��ʼ��״̬������ȡ��Ӧ��״̬�ı������״̬List
    
    // ÿ���л��������ᵼ�����¼���״̬��
    private void Awake()
    {
        //��һ��һ�еģ�Ȼ�����ֵ�
        fsm = new List<NPCState>();
        print($"{GameMgr.GetInstance().sceneName}/StateMachine/{gameObject.name}");
        TextAsset ta = (TextAsset)Resources.Load($"{GameMgr.GetInstance().sceneName}/StateMachine/" + gameObject.name);
        // print(ta.text);
        string[] taLines = ta.text.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        // �������ֻ��һ�� Init�Ļ����ʹ���
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

    // ÿ��״̬ת�������ã����ڼ����״̬�Ƿ��й̶�ʱ����ת�����״̬�Ĳ������еĻ�������� StartCoroutine
    private void CheckState()
    {
        // print("����CheckState");
        // ����״̬��
        foreach(NPCState state in fsm)
        {
            if (currentState.Equals(state.currentState))
            {
                float t;
                if (float.TryParse(state.condition, out t))
                {
                    // print("����Э��");
                    StartCoroutine(ChangeStateByTime(t, state.targetState));
                }
                
            }
        }
    }

    public void ChangeState(string condition)
    {
        // print("����ChangeState");
        foreach (NPCState state in fsm)
        {
            if (currentState.Equals(state.currentState))
            {
                if (condition.Equals(state.condition))
                {
                    // �������ȫ���ǲ��׵�
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
