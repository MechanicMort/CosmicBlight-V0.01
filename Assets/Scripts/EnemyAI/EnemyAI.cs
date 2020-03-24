using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //For enemy FOV
    [Header("Detection range")]
    [Range(0, 360)] public float viewAngle;
    public float viewDistance;
    private NavMeshAgent enemyNav;
    public Transform player;

    [Header("Patrol")]
    public bool isPatrol;
    public Transform[] points;
    private int destPoint = 0;

    //For detection
    [Header("Other")]
    public LayerMask targetMask;
    public LayerMask wallMask;
    //public LayerMask enemyLayer;
    public bool playerDetected = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyNav = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        
        Patrol();
        PlayerDetection();

        if (playerDetected)
        {
            transform.LookAt(player.position);
            enemyNav.SetDestination(player.position);
        }

    }

    private void PlayerDetection()
    {
        Collider[] playersInView = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < playersInView.Length; i++)
        {
            Transform target = playersInView[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, wallMask))
                {
                    playerDetected = true;
                }
                else
                {
                    playerDetected = false;
                }
            }

        }
    }

    public void Patrol()
    {
        if (isPatrol)
        {
            if (!playerDetected)
            {
                enemyNav.stoppingDistance = 1f;
                if (!enemyNav.pathPending && enemyNav.remainingDistance < 0.5f)
                {
                    GoToNextPoint();
                }
            }
          }
            
        
    }

    public void GoToNextPoint()
    {
        if (points.Length == 0)
            return;

        enemyNav.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
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
