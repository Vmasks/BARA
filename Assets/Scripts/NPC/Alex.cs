using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alex : NPCMood
{
    private void Awake()
    {
        nPCMoods = new string[] { "Init", "Normal", "Bother", "Mad" };
        currentMood = "Init";
    }

    public override void ChangeMood()
    {      
        switch (currentMood)
        {
            case "Init":
                currentMood = "Bother";
                Invoke("ChangeToNormal", 20.0f);

                print("hehe");
                break;
            case "Bother":
                currentMood = "Mad";
                break;
        }
    }

    //private IEnumerator ChangeToNormal_(float sec)
    //{
    //    yield return new WaitForSeconds(sec);
    //    print("haha");
    //}

    private void ChangeToNormal()
    {
        if (currentMood == "Bother")
        {
            print("hahaha");
            currentMood = "Normal";
            //记录情绪
            if (Global.npcMood.ContainsKey(Global.currentInteractNPC.name))
            {
                Global.npcMood[Global.currentInteractNPC.name] = Global.currentInteractNPC.GetComponent<NPCMood>().GetCurrentMood();
            }
            else
            {
                Global.npcMood.Add(Global.currentInteractNPC.name, Global.currentInteractNPC.GetComponent<NPCMood>().GetCurrentMood());
            }
        }
    }

}
