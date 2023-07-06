using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Serialization;

public class BaseGame : MonoSingleton<BaseGame>, IGame
{
    public Result resultsScreen;


    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void HandleHit(IHitable object1, GameObject object2)
    {
        object1.HandleHit(object2);
    }


    public virtual void GameOver(int score)
    {
        resultsScreen.gameObject.SetActive(true);
        resultsScreen.SetScore(PlayerPrefs.GetString("Name"), score);
    }
}