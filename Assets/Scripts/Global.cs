using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 这里其实都定义成静态变量是有问题的，一旦玩家玩过某个存档之后，再出去新建游戏，这里的变量都不会变，不过好在目前我的逻辑是不允许返回，
// 想重新读档或者开始游戏的话必需退出游戏重进，这样的话这些变量就都重置了，所以没问题。


public class Global
{
    /// <summary>
    /// 当前存档游戏时间
    /// </summary>
    public static float gameTime = 0f;
    /// <summary>
    /// 玩家自定义角色姓名
    /// </summary>
    //public static string name = "Player";
    /// <summary>
    /// 玩家选择的角色
    /// </summary>
    //public static string player = "Adam";
    //存储当前聊天NPC的gameObject，除了要用到它的名字，还要改变它的情绪
    public static GameObject currentTalkNPC;
    //存储所有对话内容 key：NPC名字，Value: 对话内容
    public static Dictionary<string, List<string>> dialogCollection = new Dictionary<string, List<string>>();
    //记录玩家写下的需求，Key：InputElem的name Value：该条需求
    public static Dictionary<string, string> requirements = new Dictionary<string, string>();
    //记录所有已访问NPC情绪，载入游戏存档时使用 Key: NPC名字 Value：NPC状态
    public static Dictionary<string, string> npcMood = new Dictionary<string, string>();
    //指示显示谁的详细对话内容，未来应当用UI间传参取代
    //public static string detailName = "";
    //指示是“对话”还是“观察”，未来应当用UI间传参取代
    public static bool isTalk = true;
    //当前存档名称（如果是新游戏，则值为Default）
    public static string storeName = "Default";
    //该变量用于防止玩家用键盘关闭SelectPanel时再次打开该面板。
    public static bool allowOpenSelectPanel = true;

    // 该变量用于指示玩家是否打开过提示面板
    public static bool hasOpenTipsPanel = false;

    // 该变量用于存储已经对话过的NPC的状态，最终也要保存到存档里。
    public static Dictionary<string, HashSet<string>> hasCollectedMood = new Dictionary<string, HashSet<string>>();

    // Log
    public static List<string> detailedLog = new List<string>();
    public static List<string> simpleLog = new List<string>();

    public static void AddDetailedLog(string content)
    {
        detailedLog.Add(GetLogHead() + content);
        //foreach (string log in detailedLog)
        //{
        //    Debug.Log(log);
        //}
    }

    public static void AddSimpleLog(string content)
    {
        simpleLog.Add(GetLogHead() + content);
    }

    public static string GetLogHead()
    {
        string dateTime = DateTime.Now.ToString();
        float h = Mathf.Floor(gameTime / 3600);
        float m = Mathf.Floor((gameTime % 3600) / 60);
        float s = Mathf.Floor(gameTime % 60);
        // 2022/3/20 12:18:43 0:0:7
        // Debug.Log(DateTime.Now.ToString() + " " + $"{h}:{m}:{s}");
        return DateTime.Now.ToString() + " " + $"{h}:{m}:{s}" + " ";
    }

}