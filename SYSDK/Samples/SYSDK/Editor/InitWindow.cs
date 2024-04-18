using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class InitWindow : EditorWindow
{
    InitWindow()
    {
        titleContent = new GUIContent("工程初始化");
    }

    [MenuItem("SY Framework/Init")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<InitWindow>();
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        GUILayout.Label("初始化工程");

        if (GUILayout.Button("执行"))
        {
            Init();
        }
        
        
    }

    /// <summary>
    /// 对工程目录进行初始化操作
    /// </summary>
    private static void Init()
    {
        string rootDir = Application.dataPath;
        
        List<string> dirs = new List<string>{ "Scripts" , "Scripts/Architecture" , "Scripts/Model" , "Scripts/ViewController" , "Scripts/System" , "Scripts/Utility" };

        Debug.Log("SY Framework: Init!");
        
        foreach (var dir in dirs)
        {
            var path = rootDir + "/" + dir;
            
            //Debug.Log(path);
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        
        //刷新Asset
        AssetDatabase.Refresh();
    }
    
}
