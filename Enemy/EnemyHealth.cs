using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;// when enemy die   this the speed they sink in floor
    public int scoreValue = 10; // score value
    public AudioClip deathClip; // what to play when they die


    Animator anim;//ref anmi
    AudioSource enemyAudio;//ref audio
    ParticleSystem hitParticles;//ref hitparticles
    CapsuleCollider capsuleCollider;// ref to capsuleCollider
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();//setup ref
        enemyAudio = GetComponent <AudioSource> ();//setup ref
        hitParticles = GetComponentInChildren <ParticleSystem> ();//setup ref that in childern 
        capsuleCollider = GetComponent <CapsuleCollider> ();//setup ref

        currentHealth = startingHealth;
    }


    void Update ()
    {
        // check if sinking  (direction * as fast  *time)
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();//play hit sound

        currentHealth -= amount; // decrease health
            
        hitParticles.transform.position = hitPoint;// moves hitPoint where ever it gets hit
        hitParticles.Play();// fluff come out

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true; //

        anim.SetTrigger ("Dead");// dead anim

        enemyAudio.clip = deathClip;// change audio to play death
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;// set one compentennt off 
        GetComponent <Rigidbody> ().isKinematic = true; // when 
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f); // Destry the bear to
         
    }
}
