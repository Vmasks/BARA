using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class FollowPlayer : MonoBehaviour
{
    private void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<CinemachineVirtualCamera>().Follow = Player.transform;
    }
}
