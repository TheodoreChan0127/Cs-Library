using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class AchievenmentSystem : AbstractSystem 
{
    protected override void OnInit()
    {
        this.GetModel<GameDataModel>()
            .GameData.Score
            .RegisterWithInitValue(val =>
            {
                if (val==4)
                {
                    Debug.Log("牛逼");
                }
            });
    }
}
