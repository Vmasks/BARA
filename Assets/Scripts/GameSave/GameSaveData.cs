using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSave
{
    public class GameSaveData
    {
        //存档名
        public string storeName;
        // 场景名 当前：Gym, Lib
        public string globalSceneName;
        //游戏时长
        public float gameTime;
        //存档时间
        public System.DateTime saveTime;
        //玩家自定义角色姓名
        public string name;
        //玩家选择的角色
        public string player;
        //玩家保存时所在场景名
        public string sceneName;
        //玩家位置信息
        public float posX;
        public float posY;
        //玩家收集到的对话
        public Dictionary<string, List<string>> allDialogue;
        //玩家写下的需求
        public Dictionary<string, string> requirements;
        //NPC情绪
        public Dictionary<string, string> npcMood;

        // 该变量用于指示玩家是否打开过提示面板
        public bool hasOpenTipsPanel;

        public Dictionary<string, HashSet<string>> hasCollectedMood;

        // Log
        public List<string> detailedLog;
        public List<string> simpleLog;
    }
}


