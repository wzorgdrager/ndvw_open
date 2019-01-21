using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    private Animator mAnimator;
	// Use this for initialization
	void Start () {
        mAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        bool walking = Input.GetKey(KeyCode.W);

        mAnimator.SetBool("walking", walking);

        if (Input.GetKeyDown(KeyCode.A)) {
            mAnimator.SetTrigger("fight");
        }
	}
}
