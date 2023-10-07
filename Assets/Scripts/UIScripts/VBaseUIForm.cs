using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;

public class VBaseUIForm : BaseUIForm
{
    public override void Display()
    {
        base.Display();
        if (CurrentUIType.UIForms_Type == UIFormType.PopUp)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
            string s = new string(' ', 30);
        }
        //print(UIManager.GetInstance().StackCount);
            
    }
    public override void Hiding()
    {
        base.Hiding();
        if (CurrentUIType.UIForms_Type == UIFormType.PopUp && UIManager.GetInstance().StackCount == 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;
        }
        //print(UIManager.GetInstance().StackCount);
            
    }

}
