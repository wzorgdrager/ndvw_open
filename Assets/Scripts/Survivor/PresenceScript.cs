using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenceScript : MonoBehaviour {

    private SearchFood searchFoodScript;

	// Use this for initialization
	void Start () {
        searchFoodScript = GetComponentInParent<SearchFood>();
	}
	
    private void OnTriggerStay(Collider other)
    {
        var go = other.gameObject;
        if (go.tag == "foodspawn")
        {
            if (searchFoodScript.visitedAreas[go] != SearchFood.State.VISITED)
            {
                searchFoodScript.visitedAreas[go] = SearchFood.State.VISITING;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var go = other.gameObject;
        if (go.tag == "foodspawn")
        {
            if (searchFoodScript.visitedAreas[go] != SearchFood.State.VISITED)
            {
                searchFoodScript.visitedAreas[go] = SearchFood.State.NOT_VISITED;
            }
        }
    }

}
