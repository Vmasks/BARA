using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Librarian : NPCMood
{
    //在对话刚开始时的情绪，保存下来，防止之后情绪随时间变化
    private string initMood;
    private void Awake()
    {
        nPCMoods = new string[] { "Normal", "Mad" };
        currentMood = "Normal";
        InvokeRepeating("AutoChangeMood", 30.0f, 30.0f);
    }

    public override string GetCurrentMood()
    {
        initMood = currentMood;
        return base.GetCurrentMood();
    }

    public void AutoChangeMood()
    {
        switch(currentMood)
        {
            case "Normal":
                currentMood = "Mad";
                break;
            case "Mad":
                currentMood = "Normal";
                break;
        }
    }
    public override void ChangeMood()
    {
        if (initMood == "Mad")
        {
            currentMood = "Normal";
            CancelInvoke("AutoChangeMood");
        }
    }
}
