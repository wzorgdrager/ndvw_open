using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingObjectInSight : ObjectInSight {

    public float smellRange = 4f;

    public LivingObjectInSight()
    {
        tagNames.Add("Player");
        tagNames.Add("Survivor");
    }

    public override void CheckSense(GameObject other)
    {
        if (objectsInSight.ContainsKey(other))
        {
            objectsInSight[other] = false;

            if (ObjectInLineOfSight(other) || ObjectInSmellRange(other))
            {
                objectsInSight[other] = true;
            }
        }
    }

    private bool ObjectInSmellRange(GameObject other)
    {
        return CalculatePathLength(agent, transform.position, other.transform.position) <= smellRange;
    }



}
