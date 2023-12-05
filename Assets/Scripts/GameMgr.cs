using SUIFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// 这是一个 Game session 的 Manager, 而不是全局的 Manager
public class GameMgr : MonoBehaviour
{
    // 单例模式
    private static GameMgr _Instance;
    public static GameMgr GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new GameObject("_GameMgr").AddComponent<GameMgr>();
        }
        return _Instance;
    }
    // 玩家给自己起的名字
    public string playerName = "Player";
    // 玩家选择的主角, 目前有4种选择
    public string mainCharacter = "Adam";
    // 场景, 目前有两个场景, 分别为图书馆 和 体育馆
    public string sceneName = "Gym";

    // 载入场景, 开始游戏
    public void StartGame()
    {
        // 根据场景名加载不同场景，并将主角放到不同位置
        if (sceneName == "Gym")
        {
            SceneManager.LoadScene("gym1");
            GameObject player = Resources.Load<GameObject>("Player/" + mainCharacter);
            Instantiate(player, new Vector3(10f, -8, 0), new Quaternion());
        }
        else if (sceneName == "Lib")
        {
            SceneManager.LoadScene("MainScene");
            GameObject player = Resources.Load<GameObject>("Player/" + mainCharacter);
            Instantiate(player, new Vector3(-2.5f, -3f, 0), new Quaternion());
        }
        else if (sceneName == "UCD")
        {
            SceneManager.LoadScene("Hall");
            GameObject player = Resources.Load<GameObject>("Player/" + mainCharacter);
            Instantiate(player, new Vector3(2.73f, -8.98f, 0), new Quaternion());
        }
        UIManager.GetInstance().ShowUIForms("MainPanel");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 在载入场景的时候就刷新状态
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        // 当前场景中所有NPC GameObject 的引用
        var NPCObjects = GameObject.FindGameObjectsWithTag("NPC");
        var objectCount = NPCObjects.Length;
        for (int i = 0; i < objectCount; i++)
        {
            var obj = NPCObjects[i];
            if (Global.npcMood.ContainsKey(obj.name))
            {
                obj.GetComponent<NPCMoodTest>().SetState(Global.npcMood[obj.name]);
            }
        }
        Global.AddDetailedLog($"进入场景 {scene.name}");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // 全局快捷键在这里检测
    void Update()
    {
       
    }
}
