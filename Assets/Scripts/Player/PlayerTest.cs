using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;

public class PlayerTest : MonoBehaviour
{
    [SerializeField]
    private string currentTalkNPC = "";
    Collider2D col2d;
    // Start is called before the first frame update
    void Start()
    {
        col2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTalkNPC != "" && Input.GetKey(KeyCode.Return) && Global.allowOpenSelectPanel)
        {
            UIManager.GetInstance().ShowUIForms("SelectPanel");
        }   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("NPC"))
        {
            currentTalkNPC = collision.collider.name;
            Global.currentTalkNPC = collision.gameObject;
            //检查是否需要重新设置状态，若需要，则设置
            //字典里有说明它的情绪曾改变过，需要重新设置，设置后再从字典中删除即可
            // 这个功能是必要的，但是我现在把他移动到了GameMgr 中，在每次场景变化时直接遍历场景中的全部NPC，直接赋值
            //if (Global.npcMood.ContainsKey(currentTalkNPC))
            //{
            //    collision.gameObject.GetComponent<NPCMoodTest>().SetState(Global.npcMood[collision.collider.name]);
            //    // Global.npcMood.Remove(collision.collider.name);
            //}
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentTalkNPC = "";
    }
}
