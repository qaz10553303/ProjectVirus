using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private GameObject wp;
    private List<Transform> wayPointsList = new List<Transform>();
    private Transform moveTarget;
    private Transform atkTarget;
    private int targetIndex;
    private float fireCountdown;
    private float turnSpeed = 10f;

    public Transform firePoint;
    public GameObject bulletPrefab;
    

    public GameObject canvas;
    public Slider hpBar;
    public GameObject resourceDropPrefab;
    public float moveSpeed;
    public float slowRate = 0f;
    public bool isLoop;
    public bool isDestroyOnArrive;

    public float maxHp = 100;
    public float hp = 100;
    public float attackDmg=10;
    public float attackRange = 4;
    public float fireRate = 0.3f;

    // Start is called before the first frame update

    public enum EnemyType
    {
        ENEMY1,
        ENEMY2,
        ENEMY3,
    }
    public EnemyType type;


    void Start()
    {
        SetWayPoints();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = hp / maxHp;
        DeathCheck();
        Attack();
        Move();
    }

    void SetWayPoints()
    {
        wp = GameObject.FindGameObjectWithTag("WayPoints");
        for (int i = 0; i < wp.transform.childCount; i++)
        {
            wayPointsList.Add(wp.transform.GetChild(i));
        }
        moveTarget = wayPointsList[0];
    }

    void Move()
    {
        //Debug.Log("Currently moving to: " + targetIndex);
        transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, moveSpeed*Mathf.Max((1-slowRate),0.5f) * Time.deltaTime);//maximum slowrate is 0.5
        if (Vector3.Distance(transform.position, moveTarget.position) < 0.05f)//adjust this value when having performance issues on mobile.
        {
            if (targetIndex < wayPointsList.Count - 1)//move to the next target
            {
                targetIndex += 1;
                moveTarget = wayPointsList[targetIndex];
            }
            else if (isLoop)//last waypoint->first waypoint
            {
                targetIndex = 0;
                moveTarget = wayPointsList[targetIndex];
            }

            else if (isDestroyOnArrive && targetIndex == wayPointsList.Count - 1)//on base
            {
                switch (type)
                {
                    case EnemyType.ENEMY1:
                        GameManager.Instance.healthCount -= 50;
                        GameManager.Instance.resourceCount -= 20;
                        break;
                    case EnemyType.ENEMY2:
                        GameManager.Instance.healthCount -= 100;
                        break;
                    case EnemyType.ENEMY3:
                        break;
                    default:
                        break;
                }
                Destroy(gameObject);
            }
        }
    }

    public void Hit(float dmgAmount)
    {
        hp -= dmgAmount;
    }


    void DeathCheck()
    {
        if (hp <= 0)
        {
            if (Random.value<=.3)
            {
                Instantiate(resourceDropPrefab, transform.position, Quaternion.identity);
            }
            switch (type)
            {
                case EnemyType.ENEMY1:
                    GameManager.finalScore += 100;
                    break;
                case EnemyType.ENEMY2:
                    GameManager.finalScore += 200;
                    break;
                case EnemyType.ENEMY3:
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }

    void Attack()
    {

        if (type== EnemyType.ENEMY2&& GameManager.Instance.turretsList.Count > 0)
        {
            #region UpdateAtkTarget
            float shortestDistance = Mathf.Infinity;
            Turret nearestTurret = null;

            foreach (Turret turret in GameManager.Instance.turretsList)
            {
                float distanceToTurret = Vector3.Distance(transform.position, turret.transform.position);
                if (distanceToTurret < shortestDistance)
                {
                    shortestDistance = distanceToTurret;
                    nearestTurret = turret;
                }
            }

            if (nearestTurret != null && shortestDistance <= attackRange)
            {
                atkTarget = nearestTurret.transform;
            }
            else
            {
                atkTarget = null;
            }
            #endregion
            if (atkTarget)
            {
                fireCountdown -= Time.deltaTime;



                if (fireCountdown <= 0f)
                {
                    Vector3 dir = atkTarget.transform.position - transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(dir);
                    Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
                    transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                    canvas.transform.localRotation = Quaternion.Euler(0f, -rotation.y, 0f);

                    #region Shoot
                    Vector3 bulletDir = atkTarget.transform.position - transform.position;
                    GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(bulletDir));
                    Bullet bullet = bulletGo.GetComponent<Bullet>();
                    bullet.damage = this.attackDmg;

                    if (bullet != null)
                    {
                        bullet.isEnemyBullet = true;
                        bullet.Seek(atkTarget);
                    }
                    #endregion
                    fireCountdown = 1f / fireRate;
                }
            }
        }



    }




}
