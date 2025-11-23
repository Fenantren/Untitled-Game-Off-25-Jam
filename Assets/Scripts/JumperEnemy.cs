using UnityEngine;
using UnityEngine.AI;

public class JumperEnemy : MonoBehaviour
{
    PlayerController player;
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    private void Update()
    {
        if (!player) return;
        agent.SetDestination(player.transform.position);
    }
}
