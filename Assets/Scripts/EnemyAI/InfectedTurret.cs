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
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    private Vector3 origTransform;
    private Quaternion origAngle;
    private bool inSight = false;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform muzzlePoint;
    public float rangedAttackDelay;
    public float rangedBulletSpeed;

    private float timeCount = 0.0f;
    private float rangedAttackRate;
    private void Start()
    {
        origTransform = turretBarrel.transform.position;
        origAngle = turretBarrel.transform.rotation;
    }

    private void Update()
    {
        FindVisibleTarget();
    }

    void FindVisibleTarget()
    {
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInView.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, targetsInView[i].transform.position);
            Transform target = targetsInView[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2 && inSight == false)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                {
                    inSight = true;
                }
            }

            else if (dist > viewRadius)
            {
                timeCount = timeCount + Time.deltaTime;
                turretBarrel.transform.rotation = Quaternion.RotateTowards(transform.rotation, origAngle, 200000f );
                inSight = false;
            }
            else if (inSight)
            {
                turretBarrel.transform.LookAt(target.transform.position);
                Shooting();
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
        }
    }

   
}
