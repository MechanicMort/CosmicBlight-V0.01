using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedTurret : MonoBehaviour
{
    public GameObject target;

    public GameObject turretBarrel;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public bool inSight = false;

    public Vector3 origTransform;
    public Quaternion origAngle;
    private float timeCount = 0.0f;

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

   
}
