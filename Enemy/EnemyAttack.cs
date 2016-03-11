using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{

    //amount of time between each attack
    public float timeBetweenAttacks = 0.5f;

    //how much dam each attack
    public int attackDamage = 10;


    Animator anim; //ref anim
    GameObject player;// ref player
    PlayerHealth playerHealth; // ref to playerHealth script
    EnemyHealth enemyHealth;// ref the enemyHealth script
    bool playerInRange; // is player in range to attack
    float timer;// make sure enemy doesnt attact to fast over slow


    void Awake ()
    {
        // Store ref in var and ref player health in var
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        Debug.Log(player);
        Debug.Log(playerHealth);
    }


    // on trigger is colider with other
    void OnTriggerEnter (Collider other)
    {
        //if other = player 
        if(other.gameObject == player)
        {
            // turn playerInRange True
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        //if the thing that left the tri = player  playerinrange = false
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        // Say how much much has passed
        timer += Time.deltaTime;


        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        //if player die tuen on anim PlayerDead.
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        //reset timer
        timer = 0f;

        //if player alive    take away health
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
