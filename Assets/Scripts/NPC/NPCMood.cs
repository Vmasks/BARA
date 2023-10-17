using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMood : MonoBehaviour
{
    protected string[] nPCMoods;
    public string currentMood = "Normal";
    public virtual string GetCurrentMood()
    {
        return currentMood;
    }
    public virtual void ChangeMood() { }
    //设置NPC的情绪，用于读取存档后恢复
    public void SetMood(string mood)
    {
        currentMood = mood;   
    }
}
