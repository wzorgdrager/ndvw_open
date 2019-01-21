using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAround : Action {

    public float wanderRadius = 10;
    public float wanderTimer = 2;
    public float speed = 1;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }


    protected override Status BUpdate()
    {
        Debug.Log(gameObject.name + ": wandering around");
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            var targetArea = transform.position + transform.forward * wanderRadius/2;
            Vector3 newPos = RandomNavSphere(targetArea, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
            agent.speed = speed;
        }

        return Status.SUCCESS;
    }

}
