using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using UnityEngine.UI;


public class SelectNpcPanel : VBaseUIForm
{
    [SerializeField]
    private GameObject npcArea, characterElem;

    private List<string> alreadyExist;

    private void Awake()
    {
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
       
        RigisterButtonObjectEvent("ExitBtn",p => {
            CloseUIForm();
        });

        alreadyExist = new List<string>();

    }
    private void OnEnable()
    {
      
        if (npcArea.transform.childCount < Global.dialogCollection.Count)
        {
            ReceiveMessage("SelectNpcPanelMessage", p => {
                
                SelectNpcPanelMessage snpm = p.Values as SelectNpcPanelMessage;

                foreach (string npcName in Global.dialogCollection.Keys)
                {
                    if (!alreadyExist.Contains(npcName))
                    {
                        GameObject clone = Instantiate(characterElem, npcArea.transform, false);
                        clone.name = npcName;
                        clone.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("avator/" + npcName);
                        clone.transform.Find("Text").GetComponent<Text>().text = Show(npcName);
        
                        clone.AddComponent<Button>().onClick.AddListener(() => 
                        {
                            snpm.cd(npcName);
                            CloseUIForm();
                        });
                        alreadyExist.Add(npcName);
                    }

                }
                


            });


        }


    }
}
