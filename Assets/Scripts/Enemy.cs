using System;
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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
      Debug.Log(pointValue);
      OnEnemyDestroyed?.Invoke(this);
      Destroy(this.gameObject);
      Destroy(collision.gameObject);
    }
}
