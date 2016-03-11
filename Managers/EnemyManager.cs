using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f; // how fast they spawn
    public Transform[] spawnPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);//  Spawn , wait before starting, Time before spawn
    }


    void Spawn ()
    {
        //check if player has health
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }
        //is a array  that you can spawn it different point  it you had different spawn points.
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        //Create  the ( thing to spawn, where to spawn, and rotation
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
