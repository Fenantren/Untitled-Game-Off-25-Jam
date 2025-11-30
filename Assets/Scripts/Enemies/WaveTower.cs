using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WaveTower : MonoBehaviour
{
    [SerializeField] GameObject wavePrefab;
    [SerializeField] float spawnDelay = 3f;
    [SerializeField] Transform waveSpawnPos;

    
    PlayerController player;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
        StartCoroutine(SpawnWaveRoutine());
        
    }
    

    IEnumerator SpawnWaveRoutine()
    {
        while(player)
        {
            Instantiate(wavePrefab, waveSpawnPos.position, transform.rotation);
            yield return new WaitForSeconds(spawnDelay);

        }

    }
}
