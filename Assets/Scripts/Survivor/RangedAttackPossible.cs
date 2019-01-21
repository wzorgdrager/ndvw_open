using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackPossible : Condition
{

    protected Dictionary<GameObject, PerceptionScript.LastSeen> objectsInSight;

    Dictionary<GameObject, bool> _objectsInRangedAttackRange;
    public Dictionary<GameObject, bool> objectsInRangedAttackRange
    {
        get
        {
            if (_objectsInRangedAttackRange == null)
            {
                _objectsInRangedAttackRange = new Dictionary<GameObject, bool>();
            }
            return _objectsInRangedAttackRange;
        }
    }

    private Weapon weapon;

    // Use this for initialization
    void Start()
    {
        weapon = GetComponent<Weapon>();
        var perception = GetComponentInChildren<PerceptionScript>();
        objectsInSight = perception.objectsLastSeen;

        foreach (var go in objectsInSight.Keys)
        {
            objectsInRangedAttackRange.Add(go, false);
        }
    }

    protected override Status BUpdate()
    {
        if (weapon == null || weapon.ammo <= 0)
            return Status.FAILURE;

        var GOs = objectsInSight.Keys;
        foreach (var go in GOs)
        {
            var canShootOnObject = false;
            if (objectsInSight[go].inSight == true)
            {
                if (Vector3.Distance(transform.position, go.transform.position) <= weapon.range)
                {
                    canShootOnObject = true;
                }
            }
            objectsInRangedAttackRange[go] = canShootOnObject;
        }
        return objectsInRangedAttackRange.ContainsValue(true) ? Status.SUCCESS : Status.FAILURE;
    }
}
