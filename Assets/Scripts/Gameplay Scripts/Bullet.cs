using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IHitable
{
    private Rigidbody rb;
    [SerializeField] private float shootPower;
    [SerializeField] private float resetTime = 3f;
    private bool isResetted;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        isResetted = false;
        StartCoroutine(Reset());
    }

    // Update is called once per frame

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        if (!isResetted)
        {
            isResetted = true;
            this.gameObject.SetActive(false);
        }
    }

    public void Shoot(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * shootPower, ForceMode.Impulse);
    }

    public void HandleHit(GameObject object1)
    {
        if (object1.CompareTag("Mushroom") || object1.CompareTag("Enemy"))
        {
            isResetted = true;
            this.gameObject.SetActive(false);
        }
    }
}