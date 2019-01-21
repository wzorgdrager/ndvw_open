using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInMeleeAttackRange : ObjectInSight
{

    public ObjectInMeleeAttackRange()
    {
        tagNames.Add("Player");
        tagNames.Add("zombie");
    }

    private void OnTriggerStay(Collider other)
    {
        if (objectsInSight.ContainsKey(other.gameObject))
        {
            objectsInSight[other.gameObject] = true;
        }
    }

}
