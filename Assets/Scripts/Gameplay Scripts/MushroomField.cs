using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using Random = UnityEngine.Random;

public class MushroomField : MonoBehaviour
{
    private const int MUSHROOM_COUNT_LANDSCAPE = 25;
    private const float X_SCREEN_SPACE = 0.05f, Y_SCREEN_SPACE = 0.95f;
    private const int MIN_MUSHROOMS = 10, MAX_MUSHROOMS = 20;


    [SerializeField] private GameObject mushroom;

    [SerializeField] private int mushroomsToSpawn = 10;

    [Header("Mathematical Calculations")] [SerializeField] [Range(5, 25)]
    private float radiusCheck;


    [SerializeField] [Range(5, 200)] private float noOfRetries;
    [Header("Bounds")] [SerializeField] private List<Transform> bounds = new List<Transform>();
    private Camera cam;


    private void Awake()
    {
        GameEvents.CentipedeHitEvent += OnCentipedeHit;
    }

    private void OnDestroy()
    {
        GameEvents.CentipedeHitEvent -= OnCentipedeHit;
    }

    void Start()
    {
        cam = Camera.main;
        if (IsScreenLandscape())
            mushroomsToSpawn = MUSHROOM_COUNT_LANDSCAPE;


        SpawnMushrooms();
    }


    void SpawnMushrooms()
    {
        for (int i = 0; i < mushroomsToSpawn; i++)
        {
            Vector2 position = GetRandomPositionWithoutOverlap(radiusCheck);
            GameObject obj = Instantiate(mushroom, this.transform);
            obj.transform.position = position;
        }
    }

    Vector3 GetRandomPositionWithoutOverlap(float minDistance)
    {
        Vector3 randomPosition;
        bool isOverlap;
        int retries = 0;
        do
        {
            randomPosition =
                cam.ViewportToWorldPoint(new Vector3(Random.Range(X_SCREEN_SPACE, Y_SCREEN_SPACE),
                    Random.Range(X_SCREEN_SPACE, Y_SCREEN_SPACE), 0));
            isOverlap = Physics.CheckSphere(randomPosition, minDistance,6);
            retries++;
        } while (isOverlap && retries < noOfRetries);

        if (isOverlap)
        {
            print("Objeects are overlaping Increase NoOfTries " + noOfRetries );
        }

        return randomPosition;
    }

    bool IsScreenLandscape()
    {
        return Screen.width > Screen.height;
    }

    void ClearField()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ReSpawnMushrooms()
    {
        mushroomsToSpawn = Random.Range(MIN_MUSHROOMS, MAX_MUSHROOMS);
        ClearField();
        SpawnMushrooms();
    }

    void OnCentipedeHit(Vector3 pos)
    {
        GameObject obj = Instantiate(mushroom, pos, Quaternion.identity);
        obj.transform.parent = this.transform;
    }
}