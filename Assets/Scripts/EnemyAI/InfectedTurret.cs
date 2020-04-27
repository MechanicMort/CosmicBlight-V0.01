using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedTurret : MonoBehaviour
{
    [Header("Targeting")]
    [Tooltip("Distance that the turret can target you from")]
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public GameObject turretBarrel;
    public LayerMask playerMask;
    public LayerMask enemyMask;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    private Vector3 origTransform;
    public Quaternion origAngle;
    public bool inSight = false;
    public Quaternion lastKnownAngle;
    public Quaternion currentAngle;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform muzzlePoint;
    public float rangedAttackDelay;
    public float rangedBulletSpeed;
    [Header("Other")]
    public bool isHacked = false;
    private float rangedAttackRate;

    private Transform target;
    private void Awake()
    {
        origAngle = Quaternion.Euler(turretBarrel.transform.rotation.eulerAngles);
    }
    private void Start()
    {
        origTransform = turretBarrel.transform.position;
        //origAngle = turretBarrel.transform.rotation;
        //origAngle = Quaternion.Euler(turretBarrel.transform.rotation.eulerAngles);
       
        
       
    }

    private void Update()
    {
        currentAngle = turretBarrel.transform.rotation;
        CheckTurretStatus();
        FindVisibleTarget();
        Shoot();
       // turretBarrel.transform.rotation = Quaternion.Lerp(turretBarrel.transform.rotation, origAngle, 0.05f);
    }

    void Shoot()
    {
        if (inSight)
        {
            turretBarrel.transform.LookAt(target.transform.position);
            Shooting();
        }
        else if (!inSight)
        {
            turretBarrel.transform.rotation = Quaternion.Lerp(turretBarrel.transform.rotation, origAngle, 0.01f);
        }
    }

    void FindVisibleTarget()
    {
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInView.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, targetsInView[i].transform.position);
             target = targetsInView[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2 && inSight == false)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                {
                    inSight = true;
                }
                
            }
            if (dist > viewRadius)
            {
                inSight = false;
            }

        }

    }

    public Vector3 dirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    
    public void Shooting()
    {
        if (Time.time > rangedAttackRate)
        {
            GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.transform.position, muzzlePoint.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(muzzlePoint.transform.forward * rangedBulletSpeed);
            rangedAttackRate = Time.time + rangedAttackDelay;
            StartCoroutine(BulletDespawn(bullet));
        }
    }

    public void CheckTurretStatus()
    {
        if (isHacked)
        {
            targetMask = enemyMask;
        }
        else if (!isHacked)
        {
            targetMask = playerMask;
        }
    }

    IEnumerator BulletDespawn(GameObject bullet)
    {
        yield return new WaitForSeconds(5f);
        Destroy(bullet);
    }

   
}
