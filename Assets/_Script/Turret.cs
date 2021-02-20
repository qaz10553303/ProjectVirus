using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Turret : MonoBehaviour
{
    public GameObject towerPrefab;
    public TowerType type;
    public UpgradeUI upgradeUI;



    private Transform target;
    public string enemyTag = "Enemy";
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float turnSpeed = 10f;
    public float bulletDmg = 50f;
    public float turretHp = 100;
    public float turretMaxHp = 100;
    public float slowRate = 0f;
    public int upgradeCost = 0;
    public int plantCost = 0;
    public bool isUpgraded=false;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private LineRenderer lr;


    [System.Serializable]
    public class UpgradeUI
    {
        public GameObject canvas;
        public GameObject upgradeBtn;
        public GameObject sellBtn;
        public Slider hpBar;
    }

    public enum TowerType
    {
        NONE,
        TURRET1,
        TURRET2,
        TURRET3,
        TURRET4
    }


    void Start()
    {
        lr=SetLineRenderer(transform);
        DrawVisualAttackRange(transform, transform.position, range);
        lr.enabled = false;
        //type = TowerType.NONE;
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        DeathCheck();
        UpdateTarget();
        PrepareToAttack();
        upgradeUI.hpBar.value = turretHp / turretMaxHp;
    }

    public void UpgradeTurret()
    {
        if (GameManager.Instance.resourceCount >= upgradeCost&&!isUpgraded)
        {
            GameManager.Instance.resourceCount -= upgradeCost;
            isUpgraded = true;
            switch (type)
            {
                case TowerType.NONE:
                    break;
                case TowerType.TURRET1:
                    turretMaxHp = 200f;
                    turretHp = 200f;
                    bulletDmg = 34f;
                    fireRate = 1.5f;
                    slowRate = 0.5f;
                    break;
                case TowerType.TURRET2:
                    turretMaxHp = 200f;
                    turretHp = 200f;
                    bulletDmg = 75f;
                    fireRate = 0.5f;
                    break;
                case TowerType.TURRET3:
                    break;
                case TowerType.TURRET4:
                    break;
                default:
                    break;
            }
            ToggleUpgradeUI(false);
        }

    }

    void PrepareToAttack()
    {
        fireCountdown -= Time.deltaTime;
        if (target == null)
            return;

        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        upgradeUI.canvas.transform.localRotation= Quaternion.Euler(40f, -rotation.y, 0f);


        if (fireCountdown <= 0f)
        {
            switch (type)
            {
                case TowerType.NONE:
                    break;
                case TowerType.TURRET1:
                    Attack1();
                    break;
                case TowerType.TURRET2:
                    Attack2();
                    break;
                case TowerType.TURRET3:
                    break;
                case TowerType.TURRET4:
                    break;
                default:
                    break;
            }

            fireCountdown = 1f / fireRate;
        }
    }



    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }


    void Attack1()
    {
        Vector3 dir = target.transform.position - transform.position;
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(dir));
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        bullet.damage = this.bulletDmg;
        bullet.slowRate = this.slowRate;

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void Attack2()
    {
        List<GameObject> enemiesInRange = new List<GameObject>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= range)
            {
                enemiesInRange.Add(enemy);
            }
            else if(distanceToEnemy>range&&enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Remove(enemy);
            }
        }
        if (enemiesInRange.Count > 0)
        {
            foreach (GameObject enemy in enemiesInRange)
            {
                Vector3 dir = enemy.transform.position - transform.position;
                GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(dir));
                Bullet bullet = bulletGo.GetComponent<Bullet>();
                bullet.damage = this.bulletDmg;

                if (bullet != null)
                {
                    bullet.Seek(enemy.transform);
                }
            }
        }
    }


    public void SellTurret()
    {
        GameManager.Instance.turretsList.Remove(this);
        Destroy(gameObject);
    }


    public void ToggleUpgradeUI(bool isActive)
    {
        upgradeUI.canvas.SetActive(isActive);
        lr.enabled = isActive;
    }

    LineRenderer SetLineRenderer(Transform t)
    {
        LineRenderer lr = t.GetComponent<LineRenderer>();
        if (lr==null)
        {
            lr = t.gameObject.AddComponent<LineRenderer>();
        }
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        return lr;
    }

    void DrawVisualAttackRange(Transform t, Vector3 center, float radius)
    {
        int pointAmount = 100;//the higher the value, the smoother the circle drawn
        float eachAngle = 360f / pointAmount;
        Vector3 forward = t.forward;
        lr.positionCount = pointAmount + 1;
        for (int i = 0; i <= pointAmount; i++)
        {
            Vector3 pos = Quaternion.Euler(0f, eachAngle * i, 0f) * forward * radius + center;
            lr.SetPosition(i, pos);
        }
    }

    public void Hit(float dmgAmount)
    {
        turretHp -= dmgAmount;
    }

    void DeathCheck()
    {
        if (turretHp <= 0)
        {
            GameManager.Instance.turretsList.Remove(this);
            Destroy(gameObject);
        }
    }

}
