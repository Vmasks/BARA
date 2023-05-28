using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Animator anim;
    
    /// <summary>
    /// 用于接收用户是否按下了水平方向移动键
    /// </summary>
    private float horizontal;
    /// <summary>
    /// 用于接收用户是否按下了垂直方向移动键
    /// </summary>
    private float vertical;
    /// <summary>
    /// 角色移动速度
    /// </summary>
    private float speed = 3.0f;

    private Vector2 lookDirection = new Vector2(0, -1);  //用于确定角色朝向

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //更新用户输入
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //如果都为0说明用户未按下方向键，则不需更新朝向
        if (horizontal != 0 || vertical != 0)
        {
            lookDirection.x = horizontal;
            lookDirection.y = vertical;
        }
        anim.SetFloat("LookX", lookDirection.x);
        anim.SetFloat("LookY", lookDirection.y);
        anim.SetFloat("Speed", rigidbody2d.velocity.magnitude);

    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float unifiedSpeed = speed;
        if (Math.Abs(horizontal) == Math.Abs(vertical) && horizontal != 0)
        {
            unifiedSpeed = (float)(speed / Math.Sqrt(2));
        }
        //Vector2 position = rigidbody2d.position;
        //position.x = position.x + unifiedSpeed * horizontal * Time.deltaTime;
        //position.y = position.y + unifiedSpeed * vertical * Time.deltaTime;
        //rigidbody2d.MovePosition(position);
        rigidbody2d.velocity = new Vector2(unifiedSpeed * horizontal, unifiedSpeed * vertical);
    }


}
