using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchFood : MonoBehaviour {

    private Animator anim;

    public enum State
    {
        VISITED,
        VISITING,
        NOT_VISITED
    };

    private NavMeshAgent agent;
    public Dictionary<GameObject, State> visitedAreas;
    public UAI_PropertyBoundedFloat hunger;

    // Use this for initialization
    void Start () {
        agent = GetComponentInParent<NavMeshAgent>();
        visitedAreas = new Dictionary<GameObject, State>();
        var spawns = GameObject.FindGameObjectsWithTag("foodspawn");
        anim = GetComponent<Animator>();
        foreach (var spawn in spawns)
            visitedAreas.Add(spawn, State.NOT_VISITED);
    }

    // Update is called once per frame
    void Update () {
        SearchForArea();
    }

    protected void SearchForArea()
    {
        float min = float.PositiveInfinity;
        anim.SetBool("attack", false);
        anim.SetBool("walk",true);
        List<GameObject> areas = new List<GameObject>(visitedAreas.Keys);
        foreach (var area in areas)
        {
            if (visitedAreas[area] == State.VISITED)
                continue;

            if (visitedAreas[area] == State.VISITING)
            {
                if (SearchForFood(area) == true)
                    break;
                else
                {
                    visitedAreas[area] = State.VISITED;
                }
            }

            if (visitedAreas[area] == State.NOT_VISITED)
            {
                float pathLength = ObjectInSight.CalculatePathLength(agent, transform.position, area.transform.position);
                if (pathLength < min)
                {
                    Debug.Log(gameObject.name + ": Going to spawn location");
                    min = pathLength;
                    agent.SetDestination(area.transform.position);
                }
            }
        }
    }

    protected bool SearchForFood(GameObject area)
    {
        GOContainter containter = area.GetComponent<GOContainter>();
        if (containter.objects.Count > 0)
        {
            GameObject pieceOfFood = containter.objects[0];
            float distance = (pieceOfFood.transform.position - transform.position).magnitude;
            if (distance < 1)
            {
                Debug.Log(gameObject.name + ": Pick up");
                hunger.value -= 500;
                containter.objects.RemoveAt(0);
                Destroy(pieceOfFood);
            }
            else
            {
                Debug.Log(gameObject.name + ": Going to pick up");
                agent.SetDestination(pieceOfFood.transform.position);
            }
            return true;
        }
        return false;
    }

}
