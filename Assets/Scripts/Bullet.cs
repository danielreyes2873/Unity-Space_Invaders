using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //technique for making sure there isn't a null reference during runtime if you are going to use get component
public class Bullet : MonoBehaviour
{
  private Rigidbody2D playerShot;
  private Rigidbody2D enemyShot;

  public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
      playerShot = gameObject.GetComponent<Rigidbody2D>();
      // Fire();
    }

    // Update is called once per frame
    public void PlayerFire(GameObject shot)
    {
      playerShot = shot.GetComponent<Rigidbody2D>();
      playerShot.velocity = Vector2.up * speed; 
    }

    public void EnemyFire(GameObject shot)
    {
      //gameObject.tag = "enemy";
      enemyShot = shot.GetComponent<Rigidbody2D>();
      enemyShot.velocity = Vector2.down * speed; 
    }

    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //   Destroy(col.gameObject);
    // }
}
