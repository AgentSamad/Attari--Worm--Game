using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour, IHitable

{
    private const int MUSHROOM_POINTS = 1;
    private Renderer _renderer;
    [SerializeField] private List<Material> mushroomStates;
    private int state = 0;

    void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeState()
    {
        if (state < mushroomStates.Count - 1)
        {
            state++;
            _renderer.material = mushroomStates[state];
        }
        else
        {
            GameEvents.InvokeIncreaseScore(MUSHROOM_POINTS);
            this.gameObject.SetActive(false);
        }
    }

    public void HandleHit(GameObject other)
    {
        if (other.CompareTag("Bullet"))
        {
            ChangeState();
        }
    }
}