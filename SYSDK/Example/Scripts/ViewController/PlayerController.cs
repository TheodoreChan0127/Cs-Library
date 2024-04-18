using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour,IController
{
    [SerializeField]
    private Vector2 Direction=Vector2.zero;
    public float Speed=5f;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 接收输入
    /// </summary>
    /// <param name="input"></param>
    public void OnMove(InputValue input)
    {
        Direction = input.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (Direction != Vector2.zero)
        {
            rigidbody.AddForce(new Vector3(-Direction.y,0,Direction.x)*Speed);
        }
    }

    public IArchitecture GetArchitecture()
    {
        return RollABall.Interface;
    }
}
