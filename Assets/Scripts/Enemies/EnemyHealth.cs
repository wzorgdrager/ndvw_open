using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public AudioClip deathClip;                 // The sound to play when the enemy dies.
    public Animator animator;

    //Animator anim;                              // Reference to the animator.
    //AudioSource enemyAudio;                     // Reference to the audio source.
    //ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    bool isDead;                                // Whether the enemy is dead.


    void Awake()
    {
        // Setting up the references.
        //anim = GetComponent<Animator>();
        //enemyAudio = GetComponent<AudioSource>();
        //hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        Debug.Log(currentHealth);
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Play the hurt sound effect.
        //enemyAudio.Play();

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // Set the position of the particle system to where the hit was sustained.
        //hitParticles.transform.position = hitPoint;

        // And play the particles.
        //hitParticles.Play();

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            StartCoroutine(Death());
        }
    }


    protected IEnumerator Death()
    {
        // The enemy is dead.
        isDead = true;

        animator.SetBool("isDead", true);

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            Debug.Log(script);
            script.enabled = false;
        }
        capsuleCollider.isTrigger = true;
        transform.gameObject.tag = "dead";

        yield return new WaitForSeconds(10f);
        Destroy(gameObject);

        // Tell the animator that the enemy is dead.
        //anim.SetTrigger("Dead");

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        //enemyAudio.clip = deathClip;
        //enemyAudio.Play();
    }
}