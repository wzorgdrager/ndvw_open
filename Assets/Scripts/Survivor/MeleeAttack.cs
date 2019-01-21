using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : Action
{
    private Animator anim;
    private Dictionary<GameObject, bool> objectsInMeleeAttackRange;
    private RaycastHit hit;
    Ray ray1, ray2, ray3;
    private LayerMask ignoreLayer;
    public int damagePerHit = 20;
    private float timer = 0.0f;
    public float cooldown = 1.0f;
    private float angle = 0.15f;

    // Use this for initialization
    void Start () {
        var objectInAttackMeleeRange = GetComponentInChildren<ObjectInMeleeAttackRange>();
        objectsInMeleeAttackRange = objectInAttackMeleeRange.objectsInSight;
        anim = GetComponent<Animator>();
    }

    protected override Status BUpdate()
    {
        GameObject go = null;
        if (timer <= 0)
        {
            foreach (var tmpGo in objectsInMeleeAttackRange)
            {
                if (tmpGo.Value == true) go = tmpGo.Key;
            }
            if (go != null)
            {
                timer = cooldown;
                attack(go);
                anim.SetBool("walk", false);
                anim.SetBool("attack", true);
            }
            else
                return Status.FAILURE;
            return Status.SUCCESS;
        }
        else
        {
            timer -= Time.deltaTime;
            return Status.SUCCESS;
        }
    }

    public void FixedUpdate()
    {
        raycastForMeleeAttacks();
    }

    protected void raycastForMeleeAttacks()
    {
        ray1 = new Ray(transform.position + transform.up, transform.forward + (transform.right * angle));
        ray2 = new Ray(transform.position + transform.up, transform.forward - (transform.right * angle));
        ray3 = new Ray(transform.position + transform.up, transform.forward);

        Debug.DrawRay(ray1.origin,ray1.direction, Color.blue, 2.0f);
        Debug.DrawRay(ray2.origin, ray2.direction, Color.blue, 2.0f);
        Debug.DrawRay(ray3.origin, ray3.direction, Color.blue, 2.0f);
    }

    protected void attack(GameObject objInCollider)
    {
        
        if (Physics.Raycast(ray1, out hit, 2f, ~ignoreLayer, QueryTriggerInteraction.Ignore) || Physics.Raycast(ray1, out hit, 2f, ~ignoreLayer, QueryTriggerInteraction.Ignore) || Physics.Raycast(ray1, out hit, 2f, ~ignoreLayer, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.tag == "Survivor" || hit.transform.tag == "Player" || hit.transform.tag == "zombie")
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    anim.SetBool("idle",false);
                    anim.SetBool("walk", false);
                    anim.SetBool("attack",true);
                    enemyHealth.TakeDamage(damagePerHit, hit.point);
                }
                PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    // ... the enemy should take damage.
                    anim.SetBool("idle", false);
                    anim.SetBool("walk", false);
                    anim.SetBool("attack", true);
                    playerHealth.TakeDamage(damagePerHit);
                }
            }
        }
    }

}
