using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AttackLivingObject : Action
{
    private bool coolingDown;

    public float cooldown;
    public int damage;
    public Animator animator;
    private Dictionary<GameObject, bool> livingObjectsInAttackRange;
    public LayerMask ignoreLayer;
    private RaycastHit hit;
    Ray ray1, ray2, ray3;
    private float angle = 0.15f;
    public int damagePerHit = 20;


    // Use this for initialization
    void Start()
    {
        var livingObjectInAttackRange = GetComponentInChildren<LivingObjectInAttackRange>();
        livingObjectsInAttackRange = livingObjectInAttackRange.livingObjectsInAttackRange;
        damage = 20;
        animator = GetComponent<Animator>();
        coolingDown = false;
        cooldown = 0;
    }

    protected override Status BUpdate()
    {
        if (!coolingDown)
        {
            coolingDown = true;
            cooldown = 1.0f;
            GameObject go = null;
            foreach (var lo in livingObjectsInAttackRange)
            {
                if (lo.Value == true) go = lo.Key;
            }
            if (go != null)
            {
                Debug.Log("Attacking " + go.name);
                attack(go);
            }
            else
                return Status.FAILURE;
            return Status.SUCCESS;
        }
        else
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
                coolingDown = false;
            return Status.FAILURE;
        }
    }

    protected void raycastForMeleeAttacks()
    {
        ray1 = new Ray(transform.position + transform.up, transform.forward + (transform.right * angle));
        ray2 = new Ray(transform.position + transform.up, transform.forward - (transform.right * angle));
        ray3 = new Ray(transform.position + transform.up, transform.forward);

        Debug.DrawRay(ray1.origin, ray1.direction, Color.red, 2.0f);
        Debug.DrawRay(ray2.origin, ray2.direction, Color.red, 2.0f);
        Debug.DrawRay(ray3.origin, ray3.direction, Color.red, 2.0f);

    }

    public void FixedUpdate()
    {
        raycastForMeleeAttacks();
    }

    protected void attack(GameObject objInCollider)
    {
        Debug.DrawRay(transform.position + transform.up, transform.forward, Color.red, 2.0f);
        if (Physics.Raycast(ray1, out hit, 2f, ~ignoreLayer, QueryTriggerInteraction.Ignore) || Physics.Raycast(ray1, out hit, 2f, ~ignoreLayer, QueryTriggerInteraction.Ignore) || Physics.Raycast(ray1, out hit, 2f, ~ignoreLayer, QueryTriggerInteraction.Ignore))
        {

            if (hit.transform.tag == "Survivor" || hit.transform.tag == "Player")
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    animator.Play("attack");
                    enemyHealth.TakeDamage(damagePerHit, hit.point);
                }
                PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    // ... the enemy should take damage.
                    animator.Play("attack");
                    playerHealth.TakeDamage(damagePerHit);
                }
            }
        }
    }
}