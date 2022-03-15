using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public Bullet bulletSc;
    public float speed = 8f;
  
    private Animator playerAnimator;
    public Transform shottingOffset;
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int Died = Animator.StringToHash("Died");

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.SetTrigger(Shoot);
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
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        playerAnimator.SetTrigger(Died);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        SceneManager.LoadScene("Credits");
    }
}