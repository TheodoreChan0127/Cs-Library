using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//Unity函数库
//沈阳制作，2822907581@qq.com

namespace SYLibrary
{
    public class SYUnityLibrary : MonoBehaviour
    {
        //FTimerHandle
        //【功能】
        //强大的计时器工具，性能十分稳定。计时器有三种状态：
        //（1）初始化状态：未进行过任何计时。开启计时后进入工作状态。
        //（2）工作状态：正在进行计时。计时结束后进入完成状态。
        //（3）完成状态：已经计时完毕，此时仍保留有计时记录，可以使用IsTimerFinished()得到true。使用Clear()函数清除记录后，进入初始化状态，此时使用IsTimerFinished()将会得到false。
        //【内置函数】：
        //void ConstructTimer()
        //构造函数。必须在Awake()中使用此构造函数，否则计时器无法工作！
        //void TickTimer()
        //构造函数。必须在Update()中使用此构造函数，否则计时器无法工作！
        //void StartTimer(float InWaitTime, bool bIsMilliSecond)
        //开启计时。当计时器处于初始化或完成状态时生效。
        //bool IsTimerFinished(float InWaitTime, bool bIsMilliSecond)
        //给定时间是否已经计时完毕。当计时器处于完成状态时返回true，否则返回false。
        //bool IsInitState()
        //当前是否是初始化状态。
        //void Clear()
        //清除计时器记录。当处于完成状态时使用，清除记录后进入初始化状态。
        //void ForceFinish()
        //强制立刻完成计时。强制从工作状态转变为完成状态。
        //float GetCurrentTime(bool bIsMilliSecond)
        //获取计时器当前时间戳。
        //void BindFunction_OnTimerFinished(FTimerDelegate InFuncPtr)
        //注册一个事件。该事件会在计时器计时完成时被调用。事件应当无返回值并且无参数。
        //void BindFunction_OnTimerTick(FTimerDelegate InFuncPtr)
        //注册一个事件。该事件会在计时器的每帧（每次Update）被调用。事件应当无返回值并且无参数。
        public struct FTimerHandle
        {
            //必须在Awake()中使用ConstructTimer()
            //必须在Update()中使用TickTimer()

            float WaitTime;
            float CurrentTime;
            bool bIsStartTimer;

            //初始化：0 正在倒计时：1 倒计时结束：2
            int iTimerState;

            public delegate void FTimerDelegate();
            event FTimerDelegate OnTimerFinishedEvents;
            event FTimerDelegate OnTimerTickEvents;

            public void ConstructTimer()
            {
                CurrentTime = 0;
                iTimerState = 0;
                bIsStartTimer = false;
            }

            public void TickTimer()
            {
                if (bIsStartTimer)
                {
                    iTimerState = 1;
                    CurrentTime += Time.deltaTime;

                    if (CurrentTime >= WaitTime)
                    {
                        bIsStartTimer = false;
                        iTimerState = 2;

                        if (OnTimerFinishedEvents != null)
                        {
                            OnTimerFinishedEvents.Invoke();

                            OnTimerFinishedEvents = null;
                            OnTimerTickEvents = null;
                        }
                    }

                    if (OnTimerTickEvents != null)
                    {
                        OnTimerTickEvents.Invoke();
                    }
                }
            }

            public void StartTimer(float InWaitTime, bool bIsMilliSecond)
            {
                if (this.bIsStartTimer == false)
                {
                    this.CurrentTime = 0;
                    this.iTimerState = 0;
                    if (bIsMilliSecond)
                    {
                        this.WaitTime = InWaitTime / 1000.0f;
                    }
                    else
                    {
                        this.WaitTime = InWaitTime;
                    }
                    this.bIsStartTimer = true;
                }
            }

            public bool IsTimerFinished(float InWaitTime, bool bIsMilliSecond)
            {
                if (this.iTimerState == 2)
                {
                    if (bIsMilliSecond)
                    {
                        InWaitTime /= 1000.0f;
                    }

                    if (InWaitTime == WaitTime)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;
            }

            public bool IsInitState()
            {
                if (this.iTimerState == 0/* || this.iTimerState == 2*/)
                    return true;
                else
                    return false;
            }

            public void Clear()
            {
                CurrentTime = 0;
                iTimerState = 0;
                WaitTime = 0.0f;
                bIsStartTimer = false;
                OnTimerFinishedEvents = null;
                OnTimerTickEvents = null;
            }

            public void ForceFinish()
            {
                CurrentTime = WaitTime;
            }

            public float GetCurrentTime(bool bIsMilliSecond)
            {
                if (bIsMilliSecond)
                {
                    return CurrentTime * 1000.0f;
                }
                return CurrentTime;
            }

            public void BindFunction_OnTimerFinished(FTimerDelegate InFuncPtr)
            {
                OnTimerFinishedEvents += InFuncPtr;
            }
            public void BindFunction_OnTimerTick(FTimerDelegate InFuncPtr)
            {
                OnTimerTickEvents += InFuncPtr;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //DoOnce
        //【功能】：使某函数只在全局任何时候调用一次
        //【定义】：
        //public static object DoOnce(object ThisClass, object ThisClassInstance, string FunctionName,params object[] Params)
        //【参数】
        //ThisClass 传入当前运行时所在的类指针，一般用this传入即可
        //ThisClassInstance 代表要在此类的哪个实例中运行DoOnce，一般传入类的Instance即可
        //FunctionName  以字符串形式传入一个要执行的目标函数名称，如传入"SaySomething"
        //Params    以object[]形式传入目标函数的所有参数，如传入new object[] { "Hello" }
        //【用法举例】：
        //（1）
        //如需执行无返回值、无需参数的函数：
        //public void SayHello()
        //{
        //    Debug.Log("Hello");
        //}
        //使用
        //DoOnce(this,Instance,"SayHello",new object[]{ });
        //或者
        //DoOnce(this,Instance,"SayHello",null);
        //
        //（2）
        //如需执行无返回值、有参数的函数：
        //public void SaySomething(string something)
        //{
        //    Debug.Log(something);
        //}
        //使用
        //DoOnce(this,Instance,"SaySomething",new object[]{ "Hello" });
        //
        //（3）
        //如需执行含有返回值的函数：
        //public int[] Add(int a, int b)
        //{
        //    return new int[] { a + b };
        //}
        //使用
        //int[] retArr = (int[])(SYLib.DoOnce(this, Instance, "Add", new object[] { 1, 2 }));
        //if (retArr != null)
        //    Debug.Log( retArr[0] );
        //else
        //    Debug.Log("Function has done once,retArr[] is null!");
        //【重置】
        //DoOnceReset() 可重置所有函数

        #region DoOnce
        private delegate void VoidVoidFuncPtr();
        private static Queue QFunctionDone = new Queue();

        public static object DoOnce(object ThisClass, object ThisClassInstance, string FunctionName,params object[] Params)
        {
            if (QFunctionDone.Contains(FunctionName)) return null;
            object ret = RunTargetFunction(ThisClass, ThisClassInstance, FunctionName, Params);
            QFunctionDone.Enqueue(FunctionName);

            return ret;
        }
        public static void DoOnceReset()
        {
            Self_DoOnceVoidVoid(QFunctionDone.Clear);
        }

        private static object RunTargetFunction(object ThisClass, object ThisClassInstance, string FunctionName, params object[] Params)
        {
            object ret = ThisClass.GetType().GetMethod(FunctionName).Invoke(ThisClassInstance, Params);
            return ret;
        }

        private static void Self_DoOnceVoidVoid(VoidVoidFuncPtr SelfFunction)
        {
            if (QFunctionDone.Contains(SelfFunction)) return;
            SelfFunction();
            QFunctionDone.Enqueue(SelfFunction);
        }
        #endregion

    }
}
