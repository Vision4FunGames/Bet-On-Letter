using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Observer
{
    public static UnityEvent OnGameFinish = new UnityEvent();
    public static UnityEvent OnGameLose = new UnityEvent();
    public static UnityEvent OnGameWin = new UnityEvent();
    public static UnityEvent OnGameStart = new UnityEvent();
    public static UnityEvent OnChangeForPano = new UnityEvent();
    public static UnityEvent OnChangeForInitial = new UnityEvent();
    public static UnityEvent OnGamePause = new UnityEvent();
    public static UnityEvent OnGameContinue = new UnityEvent();
}

