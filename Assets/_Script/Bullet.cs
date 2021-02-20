using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float damage = 50f;
    public float speed = 70f;
    public float slowRate = 0f;

    public bool isEnemyBullet;
    public void Seek(Transform _target)
    {
        target = _target;
    }


    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = new Vector3();
        if (isEnemyBullet)
        {
            dir = target.GetComponent<Turret>().transform.position - transform.position;
        }
        else
        {
            dir = target.GetComponent<EnemyController>().transform.position - transform.position;
        }

        //Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);


    }

    void HitTarget()
    {
        if (isEnemyBullet)
        {
            target.GetComponent<Turret>().Hit(damage);
            Destroy(gameObject);
        }
        else
        {
            target.GetComponent<EnemyController>().slowRate += slowRate;
            target.GetComponent<EnemyController>().Hit(damage);
            Destroy(gameObject);
        }

    }
}
