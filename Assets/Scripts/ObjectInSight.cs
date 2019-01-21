using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public abstract class ObjectInSight : Condition
{
    public float fieldOfViewAngle = 110f;
    List<String> _tagNames;
    protected List<String> tagNames
    {
        get
        {
            if (_tagNames == null)
            {
                _tagNames = new List<String>();
            }
            return _tagNames;
        }
    }
    
    protected NavMeshAgent agent;

    Dictionary<GameObject, bool> _objectsInSight;
    public Dictionary<GameObject, bool> objectsInSight
    {
        get
        {
            if (_objectsInSight == null)
            {
                _objectsInSight = new Dictionary<GameObject, bool>();
            }
            return _objectsInSight;
        }
    }

    SphereCollider _col;
    protected SphereCollider col
    {
        get
        {
            if (_col == null)
            {
                _col = GetComponent<SphereCollider>();
            }
            return _col;
        }
    }

    // Use this for initialization
    protected void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();

        foreach (var tagName in tagNames)
        {
            GameObject[] GOs = GameObject.FindGameObjectsWithTag(tagName);
            foreach (var go in GOs)
            {
                objectsInSight.Add(go, false);
            }
        }
    }

    protected bool ObjectInLineOfSight(GameObject other)
    {
        Vector3 direction = other.transform.position - transform.position;
        RaycastHit hit;
        float angle = Vector3.Angle(direction, transform.forward);
        if (angle < fieldOfViewAngle * 0.5f)
        {
            if (Physics.Raycast(transform.position + transform.up, direction.normalized,
             out hit, col.radius))
            {
                if (objectsInSight.ContainsKey(hit.collider.gameObject))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public virtual void CheckSense(GameObject other)
    {
        if (objectsInSight.ContainsKey(other))
        {
            objectsInSight[other] = false;

            if (ObjectInLineOfSight(other))
            {
                objectsInSight[other] = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        CheckSense(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInSight.ContainsKey(other.gameObject))
        {
            objectsInSight[other.gameObject] = false;
        }
    }

    protected override Status BUpdate()
    {
        return objectsInSight.ContainsValue(true) ? Status.SUCCESS : Status.FAILURE;
    }

    public static float CalculatePathLength(NavMeshAgent agent, Vector3 startPos, Vector3 targetPos)
    {
        NavMeshPath path = new NavMeshPath();

        agent.CalculatePath(targetPos, path);

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = startPos;
        allWayPoints[allWayPoints.Length - 1] = targetPos;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        for (int i = 0; i < allWayPoints.Length; i++)
        {
            allWayPoints[i] = Vector3.Scale(allWayPoints[i], new Vector3(1, 0, 1));
        }

        float pathLength = 0f;

        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }

}
