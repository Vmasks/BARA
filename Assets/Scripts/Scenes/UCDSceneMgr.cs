using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UCDSceneMgr : MonoBehaviour
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
        if (src == "Hall" && tar == "Exchangeroom")
        {
            player.transform.position = new Vector3(13.5f, -14.67f, 0);
        }
        else if (src == "Classroom" && tar == "Exchangeroom")
        {
            player.transform.position = new Vector3(3.34f, -14.38f, 0);
        }
        else if (src == "Exchangeroom" && tar == "Hall")
        {
            player.transform.position = new Vector3(11.23f, -5.85f, 0);
        }
        else if (src == "Exchangeroom" && tar == "Classroom")
        {
            player.transform.position = new Vector3(12.59f, -13.01f, 0);
        }
        else if (src == "Exchangeroom" && tar == "Restroom")
        {
            player.transform.position = new Vector3(1.85f, -11.01f, 0);
        }
        else if (src == "Restroom" && tar == "Exchangeroom")
        {
            player.transform.position = new Vector3(13.99f, -14.49f, 0);
        }
        else if (src == "Restroom" && tar == "Classroom2")
        {
            player.transform.position = new Vector3(1.36f, -12.5f, 0);
        }
        else if (src == "Classroom2" && tar == "Restroom")
        {
            player.transform.position = new Vector3(12.62f, -11.01f, 0);
        }
    }

}
