using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttack : Action
{
    private NavMeshAgent agent;
    public Dictionary<GameObject, bool> objectsInRangedAttackRange;
    private float timer = 1;
    public Weapon weapon;
    public bool isAimed = false;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        weapon = GetComponent<Weapon>();
        var rangedAttackPossible = GetComponent<RangedAttackPossible>();
        objectsInRangedAttackRange = rangedAttackPossible.objectsInRangedAttackRange;
    }

    protected override Status BUpdate()
    {
        GameObject go = null;
        foreach (var tmpGo in objectsInRangedAttackRange)
        {
            if (tmpGo.Value == true) go = tmpGo.Key;
        }
        if (go != null)
        {
            agent.SetDestination(transform.position);
            RotateTowards(go.transform.position);
            if (isAimed)
            {
                Debug.Log(gameObject.name + ": Ranged attacking " + go.name);
                isAimed = false;
                timer = Time.time;
            }
            if (Time.time - timer > weapon.attackSpeed)
            {
                isAimed = true;
            }
        }
        else
            return Status.FAILURE;
        return Status.SUCCESS;
    }

    private void RotateTowards(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed/10);
    }

}
