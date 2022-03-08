using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public GameObject bullet;
  public Bullet bulletSc;
  public float speed = 8f;

  public Transform shottingOffset;
    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
        bulletSc.PlayerFire(shot);
        // Debug.Log("Bang!");

        // Destroy(shot, 3f);

      }
      
      transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
      Debug.Log("you died");
      Destroy(col.gameObject);
      Destroy(gameObject);
    }
}
