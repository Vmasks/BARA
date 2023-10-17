using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;
using GameSave;

public class ExportPanel : VBaseUIForm
{
    public Button exitBtn;
    public InputField inputField;
    public Button exportBtn;
    public Text tips;
    //ѧ��
    private string stuNum;
    private void Awake()
    {
        //��������
        CurrentUIType.UIForms_Type = UIFormType.PopUp;  //��������
        CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        //ע���˳���ť�¼�
        exitBtn.onClick.AddListener(() => { CloseUIForm(); });
        inputField.onEndEdit.AddListener(content => 
        {
            stuNum = content;
        });
        exportBtn.onClick.AddListener(() => 
        {
            if (string.IsNullOrEmpty(stuNum))
            {
        
                tips.color = new Color(209f/255, 27f/255, 46f/255);
                tips.text = "����δ��ɣ�������ѧ��";
            }
            //����
            else
            {
                //string exportStr = "";
                //exportStr += $"ѧ�ţ�{stuNum}\n";
                //exportStr += $"��ɫ����{Global.name}\n";
                //exportStr += $"��Ϸʱ����{GameSaveMgr.ConvertTimeStr(Global.gameTime)}\n";
                //exportStr += $"����ʱ�䣺{DateTime.Now}\n";
                //exportStr += $"�ѷ��ʣ�{Global.dialogCollection.Count}��\n";
                //exportStr += $"��д����{Global.requirements.Count}��\n";
                //int num = 1;
                //foreach (string req in Global.requirements.Values)
                //{
                //    exportStr += num++ +  ".\n" + req + "\n\n";
                //}
                GameSaveMgr.GetInstance().ExportResult(stuNum, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                //File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + $"/{stuNum}", GameSaveMgr.Encrypt(exportStr), Encoding.UTF8);
                //File.WriteAllText(Environment.CurrentDirectory + $"/{stuNum}", GameSaveMgr.Encrypt(exportStr), Encoding.UTF8);
                tips.color = new Color(50f / 255, 156f / 255, 50f / 255);
                tips.text = "�����ɹ�";
                Global.AddDetailedLog($"�����ɹ� {stuNum}");
                Invoke("closePanel", 2.2f);
                //print(exportStr);
            }
        });
    }
    private void OnEnable()
    {
        tips.text = "";
    }
    private void closePanel()
    {
        CloseUIForm();
    }

}
