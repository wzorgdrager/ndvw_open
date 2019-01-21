using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PerceptionScript : ObjectInSight {

    public class LastSeen
    {
        public float time;
        public Vector3 position;
        public bool inSight;

        public LastSeen(float time, Vector3 position, bool inSight)
        {
            this.time = time;
            this.position = position;
            this.inSight = inSight;
        }
    }

    public float hearRange = 2;

    Dictionary<GameObject, LastSeen> _objectsLastSeen;
    public Dictionary<GameObject, LastSeen> objectsLastSeen
    {
        get
        {
            if (_objectsLastSeen == null)
                _objectsLastSeen = new Dictionary<GameObject, LastSeen>();
            return _objectsLastSeen;
        }
    }

    public PerceptionScript()
    {
        tagNames.Add("Player");
        tagNames.Add("zombie");
    }

    protected void Start()
    {
        base.Start();
        foreach (var tagName in tagNames)
        {
            GameObject[] GOs = GameObject.FindGameObjectsWithTag(tagName);
            foreach (var go in GOs)
            {
                var state = new LastSeen(Time.time, go.transform.position, false);
                objectsLastSeen.Add(go, state);
            }
        }
    }

    protected bool ObjectInHearRange(GameObject other)
    {
        return CalculatePathLength(agent, transform.position, other.transform.position) < hearRange;
    }

    public void CheckGameObject(GameObject other)
    {
        if (objectsLastSeen.ContainsKey(other))
        {
            var inLOS = ObjectInLineOfSight(other);
            var isHeard = ObjectInHearRange(other);
            objectsLastSeen[other].inSight = inLOS;
            //Debug.Log("inLOS:" + inLOS + ", isHeard: " + isHeard);
            if (inLOS == true || isHeard == true)
            {
                objectsLastSeen[other].time = Time.time;
                objectsLastSeen[other].position = other.transform.position;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        CheckGameObject(other.gameObject);
    }

}
