using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee : MonoBehaviour
{
    public float targetRadius = 10;
    public float memoryTime = 5;
    private Animator anim;
    protected NavMeshAgent agent;
    private Dictionary<GameObject, PerceptionScript.LastSeen> objectsInSight;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        var perception = GetComponentInChildren<PerceptionScript>();
        objectsInSight = perception.objectsLastSeen;
        anim = GetComponent<Animator>();
    }

    protected void Update()
    {
        Debug.Log(gameObject.name + ": fleeing");
        Vector3 midPoint = new Vector3(0,0,0);

        anim.SetBool("attack", false);
        anim.SetBool("walk", true);
        int num = 0;
        foreach (var go in objectsInSight)
        {
            if (Time.time - go.Value.time < memoryTime)
            {
                num++;
                midPoint += go.Value.position;
            }
        }

        if (num > 0)
        {
            midPoint /= num;
            var dir = midPoint - transform.position;
            var targetArea = transform.position + (-dir.normalized) * targetRadius / 2;
            Vector3 newPos = RandomNavSphere(targetArea, targetRadius, -1);
            agent.SetDestination(newPos);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

}
