using System.Collections;
using UnityEngine;

public class EnemyWaveScript : MonoBehaviour
{
    [SerializeField] float increaseRate = 2f;
    [SerializeField] float maxSize;
    [SerializeField] int damage = 2;

    PlayerController playerControllerScript;

    private void Awake()
    {
        playerControllerScript = FindFirstObjectByType<PlayerController>();
    }
    private void Update()
    {
        
        WaveExpansion();
    }
    void WaveExpansion()
    {
        transform.localScale += new Vector3(1, 0, 1) * increaseRate * Time.deltaTime;

        if (transform.localScale.z >= maxSize)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        PlayerHealth playerHealth = collision.collider.GetComponentInParent<PlayerHealth>();

        
        
            
            playerHealth?.TakeDamage(damage);

       
        

            Debug.Log("Player took damage");
        //Play SFX and VFX

        Destroy(this.gameObject);
    }
    
    IEnumerator InsideCollisionRoutine()
    {
        yield return new WaitForSeconds(2f);

    }

    
}
