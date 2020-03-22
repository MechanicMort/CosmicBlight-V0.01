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

    [Header("Patrol")]
    public bool isPatrol;
    public Transform[] points;
    private int destPoint = 0;

    //For detection
    [Header("Other")]
   // public LayerMask targetMask;
   // public LayerMask wallMask;
    //public LayerMask enemyLayer;
    public bool playerDetected = false;

    private void Start()
    {
        enemyNav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Patrol();
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
}
