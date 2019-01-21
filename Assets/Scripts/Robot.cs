using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer.Behaviors;

[RequireComponent(typeof(SteerForPoint))]
public class Robot : MonoBehaviour {

    public SteerForPoint cSteering;
    public Animator animator;

	// Use this for initialization
	void Start () {
		cSteering = GetComponent<SteerForPoint>();
        animator = GetComponent<Animator>();
        cSteering.enabled = true;
        animator.SetFloat("speed", 5);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 robotCurPos;
        Vector3 robotTargetPos = new Vector3();

        robotCurPos = transform.position;

        robotTargetPos = cSteering.TargetPoint;
        if (Vector3.Distance(robotCurPos,robotTargetPos)<1.0)
        {
            Debug.Log("stop");
            cSteering.enabled = false;
            animator.SetFloat("speed", 0);
        }
	}
}
