using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    //挂在玩家身上 用于计时
    void Update()
    {
        Global.gameTime += Time.deltaTime;
    }
}
