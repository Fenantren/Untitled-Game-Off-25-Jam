using UnityEngine;

public class PortalScript : MonoBehaviour
{
    GameManager gameManager;
    

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<Collider>().GetComponent<PlayerHealth>();
         
        if (player)
        {

            gameManager.LoadNextLevel();
        }
    }
}
