using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SUIFW;

public class StartDialog : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIManager.GetInstance().ShowUIForms("DialogPanelBranch");
        });
    }
}
