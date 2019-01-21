using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieObjectInApproachDistance : ObjectInSight
{
    public float closeDistance = 2;

    public ZombieObjectInApproachDistance()
    {
        tagNames.Add("zombie");
    }

    private bool ObjectTooCLose(GameObject other)
    {
        if (Vector3.Distance(transform.position, other.transform.position) < closeDistance)
            return true;
        return false;
    }

    public override void CheckSense(GameObject other)
    {
        if (objectsInSight.ContainsKey(other))
        {
            objectsInSight[other] = false;

            if (ObjectInLineOfSight(other) && !ObjectTooCLose(other))
            {
                objectsInSight[other] = true;
            }
        }
    }

}
