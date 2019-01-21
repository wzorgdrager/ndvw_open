using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyObjectInSight : ObjectInSight
{
    public EnemyObjectInSight()
    {
        tagNames.Add("Player");
        tagNames.Add("zombie");
    }

}
