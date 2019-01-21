using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	[Tooltip("Furthest distance bullet will look for target")]
	public float maxDistance = 1000000;
    public int damagePerShot = 10;
	RaycastHit hit;
	[Tooltip("Prefab of wall damange hit. The object needs 'LevelPart' tag to create decal on it.")]
	public GameObject decalHitWall;
	[Tooltip("Decal will need to be sligtly infront of the wall so it doesnt cause rendeing problems so for best feel put from 0.01-0.1.")]
	public float floatInfrontOfWall;
	[Tooltip("Blood prefab particle this bullet will create upoon hitting enemy")]
	public GameObject bloodEffect;
	[Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
	public LayerMask ignoreLayer;

	/*
	* Uppon bullet creation with this script attatched,
	* bullet creates a raycast which searches for corresponding tags.
	* If raycast finds somethig it will create a decal of corresponding tag.
	*/
	void Update () {

		if(Physics.Raycast(transform.position, transform.forward,out hit, maxDistance, ~ignoreLayer, QueryTriggerInteraction.Ignore)){
			if(decalHitWall){
				if(hit.transform.tag == "Survivor" || hit.transform.tag == "zombie"){
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        // ... the enemy should take damage.
                        enemyHealth.TakeDamage(damagePerShot, hit.point);
                    }
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
					Destroy(gameObject);
				}
                else
                {
                    Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
                    Destroy(gameObject);
                }
            }		
			Destroy(gameObject);
		}
		Destroy(gameObject, 0.1f);
	}

}
