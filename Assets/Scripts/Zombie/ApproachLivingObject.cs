using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ApproachLivingObject : Action
{
    public float speed = 4;

    private Dictionary<GameObject, bool> livingObjectsInSight;
    private NavMeshAgent agent;


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        var livingObjectInSight = GetComponentInChildren<LivingObjectInSight>();
        livingObjectsInSight = livingObjectInSight.objectsInSight;
    }

    protected override Status BUpdate()
    {
        GameObject go = null;
        foreach (var lo in livingObjectsInSight)
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
