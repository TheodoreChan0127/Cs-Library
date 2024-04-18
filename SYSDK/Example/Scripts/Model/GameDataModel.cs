using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class GameDataModel : AbstractModel
{
    public GameData GameData;
    
    protected override void OnInit()
    {
        //初始化数据
        GameData = new GameData();
        GameData.Score = new BindableProperty<int>();
        GameData.Score.SetValueWithoutEvent(0);
    }
}
