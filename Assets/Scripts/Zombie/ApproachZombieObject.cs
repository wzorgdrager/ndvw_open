using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ApproachZombieObject : Action
{

    public float speed = 1;

    private Dictionary<GameObject, bool> zombieObjectsInSight;
    private NavMeshAgent agent;


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        var zombieObjectInSight = GetComponentInChildren<ZombieObjectInApproachDistance>();
        zombieObjectsInSight = zombieObjectInSight.objectsInSight;
    }

    protected override Status BUpdate()
    {
        GameObject go = null;
        foreach (var lo in zombieObjectsInSight)
        {
            if (lo.Value == true) go = lo.Key;
        }
        if (go != null)
        {
            Debug.Log(gameObject.name + ": Following " + go.name);
            agent.speed = speed;
            agent.SetDestination(go.transform.position);
        }
        else
            return Status.FAILURE;
        return Status.SUCCESS;
    }
}
