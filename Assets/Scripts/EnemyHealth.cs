using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 30f;
    [SerializeField] float currentHP;
    //damage taken per particle(due to many particles ,should be set between 0.2-0.4 in the Inspector)
    [SerializeField] float damageTaken;
    private void Awake()
    {
        currentHP = healthPoints;
        
    }

    
    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(damageTaken);
        Debug.Log(other.name);
        if( currentHP <= 0)
        {
            Debug.Log("Enemy destroyed");
            Destroy(gameObject);

        }
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
    }
}
