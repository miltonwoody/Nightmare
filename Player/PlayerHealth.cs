using UnityEngine;
using UnityEngine.UI;  // Is for UI compentant
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;  // how quick damage image shown on the screen
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim; //ref anmi
    AudioSource playerAudio; //ref player audio
    PlayerMovement playerMovement;// ref to playerMovement script
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();// ref
        playerAudio = GetComponent <AudioSource> (); //ref 
        playerMovement = GetComponent <PlayerMovement> (); //ref to Player Movement
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth; // starts game with starting Health
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            //fades dam (current color,color we like, speed of flash)
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime); 
        }
        //Setting Dam abck to false
        damaged = false;
    }

    //public so other scripts can use it.
    public void TakeDamage (int amount)
    {
        // give damage
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        //Check if player dead
        if (currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        //
        isDead = true;

        playerShooting.DisableEffects ();

        //trigger Death animi
        anim.SetTrigger ("Die");

        //play death sound
        playerAudio.clip = deathClip;
        playerAudio.Play ();

        // disable playermovement
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }
}
