using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBehavior : MonoBehaviour {

    BehaviorTree BT;

	// Use this for initialization
	void Start () {
        var objectInMeleeAttackRange = GetComponentInChildren<ObjectInMeleeAttackRange>();
        var meleeAttack = GetComponent<MeleeAttack>();
        var rangedAttackPossible = GetComponentInChildren<RangedAttackPossible>();
        var rangedAttack = GetComponent<RangedAttack>();
        var approachNearestEnemy = GetComponent<ApproachEnemyObject>();

        BehaviorTreeBuilder builder = new BehaviorTreeBuilder();
        BT = builder.Selector()
            .Sequence()
                .Condition(objectInMeleeAttackRange)
                .Action(meleeAttack)
                .Break()
            .Selector()
                .Sequence()
                    .Condition(rangedAttackPossible)
                    .Action(rangedAttack)
                    .Break()
                .Action(approachNearestEnemy)
                .Break()
            .Break()
            .End();
	}
	
	// Update is called once per frame
	void Update () {
        BT.Tick();
	}
}
