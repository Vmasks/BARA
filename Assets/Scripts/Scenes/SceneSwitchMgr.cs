using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchMgr : MonoBehaviour
{
    string entranceName, src ,tar;
    GameObject player;

    private void Awake()
    {
        entranceName = gameObject.name;
        string[] spl = entranceName.Split('_', System.StringSplitOptions.RemoveEmptyEntries);
        src = spl[0];
        tar = spl[1];
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(tar);
        if (src == "gym1" && tar == "gym2")
        {
            player.transform.position = new Vector3(15.86f, -10.78f, 0);
        }
        else if (src == "gym2" && tar == "gym1")
        {
            player.transform.position = new Vector3(17.33f, -1.14f, 0);
        }
        else if (src == "gym2" && tar == "gym3")
        {
            player.transform.position = new Vector3(16.3f, -10.5f, 0);
        }
        else if (src == "gym3" && tar == "gym2")
        {
            player.transform.position = new Vector3(6.75f, -1f, 0);
        }
    }

}
