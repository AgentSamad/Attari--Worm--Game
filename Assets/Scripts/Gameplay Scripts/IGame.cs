using UnityEngine;
using System.Collections;

public interface IGame
{
    void HandleHit(IHitable object1, GameObject object2);
    void GameOver(int score);
}