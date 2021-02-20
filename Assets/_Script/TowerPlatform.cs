using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlatform : MonoBehaviour
{
    GameObject turretToSpawn;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTurret()
    {
        if (transform.childCount == 0&& GameManager.Instance.selectedTower)//if theres no turret on this
        {
            if(GameManager.Instance.resourceCount>=GameManager.Instance.selectedTower.plantCost)
            {
                GameManager.Instance.resourceCount -= GameManager.Instance.selectedTower.plantCost;
                turretToSpawn = GameManager.Instance.selectedTower.towerPrefab;
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);//y+1 to spawn on the platform
                GameManager.Instance.turretsList.Add(Instantiate(turretToSpawn, spawnPos, Quaternion.identity, this.transform).GetComponent<Turret>());
            }
            else
            {
                Debug.Log("Not enough resource!");
            }
        }
    }
}
