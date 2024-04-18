using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

#region 数据定义

public struct GameData
{
    //public int Score;
    public BindableProperty<int> Score;
}

#endregion

#region 命令定义

public class IncreaseScoreCommand:AbstractCommand
{
    protected override void OnExecute()
    {
        //命令核心逻辑
        this.GetModel<GameDataModel>().GameData.Score.Value += this.GetUtility<MultiTen>().Get(1);
        //触发事件
        this.SendEvent<ScoreChangedEvent>();
    }
}

#endregion

#region 事件定义

public struct ScoreChangedEvent
{
    
}

#endregion

public class RollABall : Architecture<RollABall>
{
    protected override void Init()
    {
        #region 注册Model

        RegisterModel(new GameDataModel());

        #endregion

        #region 注册System

        RegisterSystem(new AchievenmentSystem());

        #endregion
        
        
        #region 注册Utility

        RegisterUtility(new MultiTen());

        #endregion
    }
}
