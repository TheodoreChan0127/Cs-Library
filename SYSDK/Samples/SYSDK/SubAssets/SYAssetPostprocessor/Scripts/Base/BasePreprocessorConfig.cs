
using System;
using UnityEngine;

public class BasePreprocessorConfig<T>:ScriptableObject
            where T : BasePreprocessorConfig<T>
{
    public static T Instance;

    private void OnValidate()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
    }
}
