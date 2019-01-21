using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour {

    BehaviorTree BT;

	// Use this for initialization
	void Start () {
        var livingObjectInAttackRange = GetComponentInChildren<LivingObjectInAttackRange>();
        var attackLivingObject = GetComponent<AttackLivingObject>();
        var livingObjectInSight = GetComponentInChildren<LivingObjectInSight>();
        var approachLivingObject = GetComponent<ApproachLivingObject>();
        var zombieObjectInSight = GetComponentInChildren<ZombieObjectInApproachDistance>();
        var approachZombieObject = GetComponent<ApproachZombieObject>();
        var wanderAround = GetComponent<WanderAround>();

        BehaviorTreeBuilder builder = new BehaviorTreeBuilder();
        BT = builder.Selector()
            .Sequence()
                .Condition(livingObjectInAttackRange)
                .Action(attackLivingObject)
                .Break()
            .Sequence()
                .Condition(livingObjectInSight)
                .Action(approachLivingObject)
                .Break()
            .Sequence()
                .Condition(zombieObjectInSight)
                .Action(approachZombieObject)
                .Break()
            .Action(wanderAround)
            .Break()
            .End();
	}
	
	// Update is called once per frame
	void Update () {
        BT.Tick();
	}
}
