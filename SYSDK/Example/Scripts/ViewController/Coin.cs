using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class Coin : MonoBehaviour,IController
{
    //需要交互的Model
    private GameDataModel gameDataModel;

    private void Start()
    {
        //获取Model
        gameDataModel = this.GetModel<GameDataModel>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player")
        {
            return;
        }
        
        //交互逻辑
        this.SendCommand<IncreaseScoreCommand>();
        
        gameObject.SetActive(false);
    }

    public IArchitecture GetArchitecture()
    {
        return RollABall.Interface;
    }
}
