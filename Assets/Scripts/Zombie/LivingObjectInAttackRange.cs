using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObjectInAttackRange : LivingObjectInSight
{

    public Dictionary<GameObject, bool> livingObjectsInAttackRange
    {
        get
        {
            return objectsInSight;
        }
    }

    private void CheckRange(Collider other)
    {
        if (livingObjectsInAttackRange.ContainsKey(other.gameObject))
        {
            livingObjectsInAttackRange[other.gameObject] = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        CheckRange(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (livingObjectsInAttackRange.ContainsKey(other.gameObject))
        {
            livingObjectsInAttackRange[other.gameObject] = false;
        }
    }

    protected override Status BUpdate()
    {
        return livingObjectsInAttackRange.ContainsValue(true) ? Status.SUCCESS : Status.FAILURE;
    }
}
