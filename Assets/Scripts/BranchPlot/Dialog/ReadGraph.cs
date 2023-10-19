using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using SUIFW;
using UnityEngine.UI;

public class ReadGraph : MonoBehaviour
{
    private void Awake() {
        UIManager.GetInstance().ShowUIForms("DialogPanelBranch");
    }
}
