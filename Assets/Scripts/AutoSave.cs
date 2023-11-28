using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSave;

public class AutoSave : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(AutoSaveEnum());
    }
    IEnumerator AutoSaveEnum()
    {
        while (true)
        {
            yield return new WaitForSeconds(120.0f);
            GameSaveMgr.GetInstance().AutoSave();
            //print("保存！");
        }
    }
}
