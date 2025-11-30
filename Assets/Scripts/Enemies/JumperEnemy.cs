using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class JumperEnemy : MonoBehaviour
{
    PlayerController player;
    NavMeshAgent agent;
    [SerializeField] GameObject enemyWavePrefab;
    [SerializeField] float spawnDelay = 2f;
    [SerializeField] Transform waveSpawnPos;

    
    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(SpawnWaveRoutine());
    }
    

    private void Update()
    {
        if (!player) return;
        agent.SetDestination(player.transform.position);

        
    }

    IEnumerator SpawnWaveRoutine()
    {
        

        while (player)
        {
            Instantiate(enemyWavePrefab, waveSpawnPos.position, waveSpawnPos.rotation);
            yield return new WaitForSeconds(spawnDelay);

        }

    }
    

    
}
