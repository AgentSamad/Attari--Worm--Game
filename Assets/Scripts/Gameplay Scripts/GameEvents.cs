using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<Vector3> CentipedeHitEvent;

    public static Action GameOverEvent;

    public static Action<int> IncreaseScore;

    public static Action NextLevel;

    public static void InvokeCentipedeHitEvent(Vector3 pos)
    {
        CentipedeHitEvent?.Invoke(pos);
    }

    public static void InvokeGameOver()
    {
        GameOverEvent?.Invoke();
    }

    public static void InvokeIncreaseScore(int score)
    {
        IncreaseScore?.Invoke(score);
    }

    public static void InvokeNextLevel()
    {
        NextLevel?.Invoke();
    }
}