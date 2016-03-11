using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;// damage bull have 
    public float timeBetweenBullets = 0.15f;// how fast bullets are 
    public float range = 100f;// how far bullet can go

    //refs 
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;// how long visionibly that effect


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable"); // help shot anything that shotable
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime; 

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        //turn off gun effect after a while
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        //turn off gun line  & ligtht
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f; //reset timer

        gunAudio.Play ();

        gunLight.enabled = true; // turn light on

        gunParticles.Stop (); // 
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position); // set the end of the line (the beginning

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        //fire ray foward (ray, give info about what you hit, range, The ray and only hit shootable thing
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            //what every you hit  get Health script
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            // if enemyhealth exist
            if (enemyHealth != null)
            {
                //
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            //the second point hit the a certain point
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            // 1,where the ray start, that the point adn * b range
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
