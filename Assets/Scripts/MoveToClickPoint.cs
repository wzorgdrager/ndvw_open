using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToClickPoint : MonoBehaviour {

    NavMeshAgent agent;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("agent.destination: " + agent.destination);
        //Debug.Log("transform.position: " + transform.position);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //Debug.Log("klik");
            if (Physics.Raycast(
                Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, 100))
            {
                //Debug.Log("hit: " + hit.collider.gameObject.name);
                agent.destination = hit.point;
            }
        }
	}
}
