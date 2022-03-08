using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int numEnemiesAcross = 6;
    public float widthPerEnemy = 2f;
    public float heightPerEnemy = 2f;
    public float secondsPerStep = 0.5f;
    public float minShootInterval = 1f;
    public float maxShootInterval = 7f;
    public Transform redEnemy;
    public Transform greenEnemy;
    public Transform blueEnemy;
    public Transform pinkEnemy;
    public Transform enemyRoot;
    public ScoreManager scoreManager;
    private Vector3 marchDirection = Vector3.right;
    private float currentShotInterval;
    private float timeSinceLastStep;
    private float timeSinceLastShot;

    private void Start()
    {
        float enemyStartHeight = 3f;
        SpawnEnemyRow(redEnemy, enemyStartHeight);
        SpawnEnemyRow(greenEnemy, enemyStartHeight - heightPerEnemy);
        SpawnEnemyRow(blueEnemy, enemyStartHeight - heightPerEnemy * 2f);
        SpawnEnemyRow(pinkEnemy, enemyStartHeight - heightPerEnemy * 3f);
        currentShotInterval = Random.Range(minShootInterval, maxShootInterval);
    }

    private void Update()
    {
        timeSinceLastStep += Time.deltaTime;
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastStep > secondsPerStep)
        {
            timeSinceLastStep -= secondsPerStep;
            enemyRoot.position += marchDirection * widthPerEnemy * 0.5f;

            float horizontalExtent = Camera.main.orthographicSize * Camera.main.aspect - widthPerEnemy;
            foreach (Transform enemyTransform in enemyRoot)
            {
                if (Mathf.Abs(enemyTransform.position.x) > horizontalExtent)
                {
                    enemyRoot.position += Vector3.down * heightPerEnemy;
                    marchDirection *= -1f;
                    Debug.Log("down");
                    break;
                }
            }
        }

        if (timeSinceLastShot > currentShotInterval)
        {
            timeSinceLastShot -= currentShotInterval;
            currentShotInterval = Random.Range(minShootInterval, maxShootInterval);
        }
        
    }

    void SpawnEnemyRow(Transform enemyPrefab, float height)
    {
        for (int i = 0; i < numEnemiesAcross; i++)
        {
            float xPos = -(numEnemiesAcross * widthPerEnemy) / 2 + i * widthPerEnemy + widthPerEnemy / 2;
            var enemy = Instantiate(enemyPrefab, new Vector3(xPos, height, 0f), Quaternion.identity);
            enemy.SetParent(enemyRoot);
            enemy.GetComponent<Enemy>().OnEnemyDestroyed += OnEnemyDied;
        }
    }

    void OnEnemyDied(Enemy enemy)
    {
        enemy.OnEnemyDestroyed -= OnEnemyDied;
        scoreManager.AddPoints(enemy.pointValue);
    }
}
