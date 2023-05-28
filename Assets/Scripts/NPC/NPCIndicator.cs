using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIndicator : MonoBehaviour
{
    [SerializeField]
    private Sprite bother, normal, mad;
    [SerializeField]
    private void Awake()
    {
        Transform indicator =  transform.Find("Indicator");
        indicator.gameObject.SetActive(false);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
