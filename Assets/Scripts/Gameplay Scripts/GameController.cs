using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : BaseGame
{
    [Header("Canvas Elements")] [SerializeField]
    private GameObject mainMenu;

    [SerializeField] private GameObject gameplay;
    [SerializeField] private InputField name;

    [Header("Objects References")] [SerializeField]
    private Centipede _centipede;

    [SerializeField] private MushroomField field;
    [SerializeField] private GameObject player;
    public Text score_text;
    private int score;

    void Start()
    {
        player.SetActive(false);

        if (PlayerPrefs.HasKey("Name"))
        {
            name.text = PlayerPrefs.GetString("Name");
        }
    }

    private void OnEnable()
    {
        GameEvents.GameOverEvent += PlayerTouched;
        GameEvents.IncreaseScore += SetScore;
        GameEvents.NextLevel += NextLevel;
    }

    private void OnDestroy()
    {
        GameEvents.GameOverEvent -= PlayerTouched;
        GameEvents.IncreaseScore -= SetScore;
        GameEvents.NextLevel -= NextLevel;
    }


    public void Play()
    {
        player.SetActive(true);
        _centipede.Respawn();
        mainMenu.SetActive(false);
        gameplay.SetActive(true);
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("Name", name.text);
    }

    public void NextLevel()
    {
        _centipede.UpdateDifficulty();
        _centipede.Respawn();
        field.ReSpawnMushrooms();
    }

    public void Retry()
    {
        _centipede.Respawn();
        field.ReSpawnMushrooms();
        resultsScreen.gameObject.SetActive(false);
        player.SetActive(true);
        gameplay.SetActive(true);
        ZeroScore();
    }

    void PlayerTouched()
    {
        GameOver(score);
    }

    public override void GameOver(int s)
    {
        base.GameOver(s);
        gameplay.SetActive(false);
        player.SetActive(false);
    }


    public void SetScore(int i)
    {
        score += i;
        score_text.text = score.ToString();
    }


    public void ZeroScore()
    {
        score = 0;
        score_text.text = score.ToString();
    }
}