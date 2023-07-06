using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Objects References")] [SerializeField]
    private Transform muzzlePoint;

    [SerializeField] private Transform poolParent;

    [Header("Prefab References")] [SerializeField]
    private Bullet bulletPrefab;

    [Header("Values")] [SerializeField] private float fireDelay;
    [SerializeField] private int poolAmount;
    private List<Bullet> bullets = new List<Bullet>();
    private float timer;

    void Start()
    {
        InitializePool();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Shoot();
                timer = fireDelay;
            }
        }
    }


    void InitializePool()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            bullets.Add(Instantiate(bulletPrefab, poolParent));
        }
    }


    Bullet GetActiveBullet()
    {
        foreach (var b in bullets)
        {
            if (!b.gameObject.activeInHierarchy)
            {
                b.gameObject.SetActive(true);
                return b;
            }
        }

        Bullet extraBullet = Instantiate(bulletPrefab, poolParent);
        bullets.Add(extraBullet);
        extraBullet.gameObject.SetActive(true);
        return extraBullet;
    }

    void Shoot()
    {
        GetActiveBullet().Shoot(muzzlePoint.position);
    }
}