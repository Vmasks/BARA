using SUIFW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// ����һ�� Game session �� Manager, ������ȫ�ֵ� Manager
public class GameMgr : MonoBehaviour
{
    // ����ģʽ
    private static GameMgr _Instance;
    public static GameMgr GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new GameObject("_GameMgr").AddComponent<GameMgr>();
        }
        return _Instance;
    }
    // ��Ҹ��Լ��������
    public string playerName = "Player";
    // ���ѡ�������, Ŀǰ��4��ѡ��
    public string mainCharacter = "Adam";
    // ����, Ŀǰ����������, �ֱ�Ϊͼ��� �� ������
    public string sceneName = "Gym";

    // ���볡��, ��ʼ��Ϸ
    public void StartGame()
    {
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
        else if (sceneName == "MyScenario")
        {
            SceneManager.LoadScene("Blank");
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
        // �����볡����ʱ���ˢ��״̬
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        // ��ǰ����������NPC GameObject ������
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
        Global.AddDetailedLog($"���볡�� {scene.name}");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // ȫ�ֿ�ݼ���������
    void Update()
    {
       
    }
}
