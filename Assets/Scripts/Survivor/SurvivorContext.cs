using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class SurvivorContext : MonoBehaviour {

    public int DesireToKill = 5;

    //agent
    UAI_Agent AI_agent;
    public UAI_PropertyBoundedFloat health, hunger, stamina, enemies_around, weapons_in_sight, desire_to_kill;

    private SearchFood SearchFoodComp;
    private Flee FleeComp; 
    private List<MonoBehaviour> FightComps = new List<MonoBehaviour>();

    private bool SearchFoodEnabled = false;
    private bool RestEnabled = false;
    private bool FightEnabled = false;
    private bool FleeEnabled = false;

    private PerceptionScript perception;
    private PresenceScript presence;

    private Animator mAnimation;

    // Use this for initialization
    void Start () {
        AI_agent = GetComponent<UAI_Agent>();
        perception = GetComponentInChildren<PerceptionScript>();
        presence = GetComponentInChildren<PresenceScript>();

        SearchFoodComp = GetComponent<SearchFood>();
        FleeComp = GetComponent<Flee>();

        FightComps.Add(GetComponent<RangedAttack>());
        FightComps.Add(GetComponent<RangedAttackPossible>());
        FightComps.Add(GetComponent<FightBehavior>());
        FightComps.Add(GetComponent<MeleeAttack>());
        FightComps.Add(GetComponent<ApproachEnemyObject>());

        AI_agent.SetVoidActionDelegate("Rest", Rest);
        AI_agent.SetVoidActionDelegate("SearchFood", SearchFood);
        AI_agent.SetVoidActionDelegate("Flee", Flee);
        AI_agent.SetVoidActionDelegate("Fight", Fight);

        desire_to_kill.value = DesireToKill;
        enemies_around.value = 0;

        mAnimation = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        AI_agent.UpdateAI();
        enemies_around.value = enemiesInSight();

        Debug.Log(enemiesInSight());
    }

    void Rest() {
        if (RestEnabled) {
            stamina.value += UtilityTime.time * 10f;
            health.value += UtilityTime.time * 10f;
            return;
        }

        Debug.Log("Now resting");

        foreach (MonoBehaviour comp in FightComps)
        {
            comp.enabled = false;
        }

        //disable search food
        SearchFoodComp.enabled = false;
        presence.enabled = false;

        //disable flee
        FleeComp.enabled = false;

        //keep track of all bools
        SearchFoodEnabled = false;
        RestEnabled = true;
        FightEnabled = false;
        FleeEnabled = false;

        mAnimation.SetBool("idle", true);
        mAnimation.SetBool("attack", false);
        mAnimation.SetBool("walk", false);
    }

    void SearchFood() {
        if (SearchFoodEnabled) {
            stamina.value -= UtilityTime.time * 5f;
            health.value += UtilityTime.time * 5f;
            hunger.value -= UtilityTime.time * 100f; //This needs to be removed!! Added this because searchfood wasnt working
            return;
        }


        Debug.Log("Now searching for food");

        foreach (MonoBehaviour comp in FightComps)
        {
            comp.enabled = false;
        }

        //enable presence
        presence.enabled = true;

        //enable search food
        SearchFoodComp.enabled = true;

        //disable flee
        FleeComp.enabled = false;

        //keep track of all bools
        SearchFoodEnabled = true;
        RestEnabled = false;
        FightEnabled = false;
        FleeEnabled = false;

        mAnimation.SetBool("idle", false);
        mAnimation.SetBool("attack", false);
        mAnimation.SetBool("walk", true);
    }

    void Flee() {
        if (FleeEnabled)
        {
            stamina.value -= UtilityTime.time * 5f;
            health.value += UtilityTime.time * 5f;
            return;
        }

        Debug.Log("Now fleeing");

        foreach (MonoBehaviour comp in FightComps)
        {
            comp.enabled = false;
        }

        //enable fleeing
        FleeComp.enabled = true;

        //disable search food
        SearchFoodComp.enabled = false;
        presence.enabled = false;

        //keep track of all bools
        SearchFoodEnabled = false;
        RestEnabled = false;
        FightEnabled = false;
        FleeEnabled = true;

        mAnimation.SetBool("idle", false);
        mAnimation.SetBool("attack", false);
        mAnimation.SetBool("walk", true);
    }

    void Fight() {
        if (FightEnabled)
        {
            stamina.value -= UtilityTime.time * 10f;
            return;
        }

        Debug.Log("Now fighting");

        foreach (MonoBehaviour comp in FightComps) {
            comp.enabled = true;
        }

        //disable fleeing
        FleeComp.enabled = false;

        //disable search food
        SearchFoodComp.enabled = false;
        presence.enabled = false;

        //keep track of all bools
        SearchFoodEnabled = false;
        RestEnabled = false;
        FightEnabled = true;
        FleeEnabled = false;

        mAnimation.SetBool("idle", false);
        mAnimation.SetBool("attack", true);
        mAnimation.SetBool("walk", false);
    }

    /**
     * Finding the enemies code
     */
    private int enemiesInSight() {
        int Count = 0;
        foreach (KeyValuePair<GameObject, PerceptionScript.LastSeen> entry in perception.objectsLastSeen)
        {
            if (Math.Abs(Time.time - entry.Value.time) < 100) Count += 1;
        }

        return Count;
    }


}
