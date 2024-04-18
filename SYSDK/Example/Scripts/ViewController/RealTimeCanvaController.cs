using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class RealTimeCanvaController : MonoBehaviour,IController
{
    private Text scoreValueText;
    private GameDataModel gameDataModel;
    
    // Start is called before the first frame update
    void Start()
    {
        //获取模型
        gameDataModel = this.GetModel<GameDataModel>();
        
        scoreValueText = GameObject.Find("ScoreValue").GetComponent<Text>();
        
        /*//注册数据更新事件
        this.RegisterEvent<ScoreChangedEvent>(e =>
        {
            //表现逻辑
            UpdateCanva();
        }).UnRegisterWhenGameObjectDestroyed(gameObject);*/

        //注册数据更新事件
        gameDataModel.GameData.Score.RegisterWithInitValue(val =>
        {
            SetText(val.ToString());
        });
    }

    public void UpdateCanva()
    {
        SetText(gameDataModel.GameData.Score.ToString());
    }

    void SetText(string content)
    {
        scoreValueText.text = content;
    }

    public IArchitecture GetArchitecture()
    {
        return RollABall.Interface;
    }
}
