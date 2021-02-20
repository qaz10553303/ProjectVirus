using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate=3;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public Transform enemyCollection;
    float timer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            int temp = Random.Range(1, 3);//1-2
            switch (temp)
            {
                case 1:
                    Instantiate(enemyPrefab1, this.transform.position, Quaternion.identity, enemyCollection);
                    break;
                case 2:
                    Instantiate(enemyPrefab2, this.transform.position, Quaternion.identity, enemyCollection);
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            GameManager.Instance.waveCount += 1;

            timer = 1 / spawnRate;
        }
    }
}
