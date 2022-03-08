﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDestroyed(Enemy enemy);
    public event EnemyDestroyed OnEnemyDestroyed;

    public int pointValue;
    public GameObject bullet;
    public Bullet bulletSc;
    public float minShootInterval = 12f;
    public float maxShootInterval = 16f;
    private float timeSinceLastShot;
    private float currentShotInterval;

    // Start is called before the first frame update
    private void Start()
    {
        switch (gameObject.name)
        {
            case ("Pink Enemy(Clone)"):
                pointValue = 5;
                break;
            case ("Blue Enemy(Clone)"):
                pointValue = 10;
                break;
            case ("Green Enemy(Clone)"):
                pointValue = 15;
                break;
            case ("Red Enemy(Clone)"):
                pointValue = 20;
                break;
        }
        currentShotInterval = Random.Range(minShootInterval, maxShootInterval);
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot > currentShotInterval)
        {
            if (FrontRow())
            {
               Fire();
            }
            timeSinceLastShot -= currentShotInterval;
            currentShotInterval = Random.Range(minShootInterval, maxShootInterval); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
      // Debug.Log(collision.gameObject.name);
      OnEnemyDestroyed?.Invoke(this);
      Destroy(this.gameObject);
      Destroy(collision.gameObject);
    }

    public void Fire()
    {
        GameObject shot = Instantiate(bullet, transform.position - new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        bulletSc.EnemyFire(shot);
    }

    bool FrontRow()
    {
        // Debug.DrawRay(transform.position, Vector3.down, Color.yellow);
        // Debug.Break();
        if (Physics2D.Raycast(transform.position - new Vector3(0f, 0.5f, 0f), Vector3.down, 1f))
        {
            // Debug.Log("false");
            return false;
        }

        // Debug.Log("true");
        return true;
        
    }
}
