using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ApproachEnemyObject : Action
{
    private NavMeshAgent agent;
    private Dictionary<GameObject, PerceptionScript.LastSeen> objectsInSight;
    private float timer = 1;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        var perception = GetComponentInChildren<PerceptionScript>();
        objectsInSight = perception.objectsLastSeen;
        anim = GetComponent<Animator>();
    }

    protected override Status BUpdate()
    {
        Vector3 target = transform.position;
        float min = float.PositiveInfinity;
        var GOs = objectsInSight.Keys;

        anim.SetBool("attack", false);
        anim.SetBool("idle",false);
        anim.SetBool("walk", true);
        foreach (var go in GOs)
        {
            var distance = PerceptionScript.CalculatePathLength(agent, transform.position, objectsInSight[go].position);
            if (distance < min)
            {
                min = distance;
                target = objectsInSight[go].position;
            }
        }
        if (min < 1)
        {
            if (Time.time - timer > 1)
            {
                timer = Time.time;
                target = Flee.RandomNavSphere(transform.position, 2, -1);
                Debug.Log(gameObject.name + ": Going to position: " + target);
                agent.SetDestination(target);
            }
        } else
        {
            Debug.Log(gameObject.name + ": Going to position: " + target);
            agent.SetDestination(target);
        }
        return Status.SUCCESS;
    }

}
