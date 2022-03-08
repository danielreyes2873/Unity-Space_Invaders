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
    public Transform enemyRoot;
    
    private Vector3 marchDirection = Vector3.right;
    private float currentShotInterval;
    private float timeSinceLastStep;
    private float timeSinceLastShot;

    private void Start()
    {
        float windowHeight = Camera.main.orthographicSize * 2;
        float enemyStartHeight = windowHeight - heightPerEnemy * 2.5f;
        SpawnEnemyRow(redEnemy, enemyStartHeight);
        SpawnEnemyRow(greenEnemy, enemyStartHeight - heightPerEnemy);
        SpawnEnemyRow(blueEnemy, enemyStartHeight - heightPerEnemy);
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
            float xPos = -(numEnemiesAcross * widthPerEnemy) / 2 + 1 * widthPerEnemy + widthPerEnemy / 2;
            Transform enemy = Instantiate(enemyPrefab, new Vector3(xPos, height, 0f), Quaternion.identity) as Transform;
            enemy.SetParent(enemyRoot);
            enemy.GetComponent<Enemy>().OnEnemyDestroyed += OnEnemyDied;
        }
    }

    void OnEnemyDied(Enemy enemy)
    {
        enemy.OnEnemyDestroyed -= OnEnemyDied;
        // ScoreManager.AddPoints(enemy.pointValue);
    }
}
