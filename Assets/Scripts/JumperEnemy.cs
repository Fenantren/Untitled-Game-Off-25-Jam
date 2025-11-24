using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class JumperEnemy : MonoBehaviour
{
    PlayerController player;
    NavMeshAgent agent;
    [SerializeField] GameObject enemyWaveParticles;
    [SerializeField] float spawnDelay = 2f;
    [SerializeField] Transform waveSpawnPos;

    [SerializeField] float sphereCastRadius = 5f;
    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(SpawnWaveRoutine());
    }
    private void Start()
    {
    }

    private void Update()
    {
        if (!player) return;
        agent.SetDestination(player.transform.position);

        /*RaycastHit hit;
        
        if(Physics.SphereCast(this.transform.position, sphereCastRadius,Vector3.zero, out hit, sphereCastRadius, 8))
        {
            agent.SetDestination(player.transform.position);
        }*/
    }

    IEnumerator SpawnWaveRoutine()
    {
        while (player)
        {
            Instantiate(enemyWaveParticles, waveSpawnPos.position, waveSpawnPos.rotation);
            yield return new WaitForSeconds(spawnDelay);

        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereCastRadius);
    }

    
}
