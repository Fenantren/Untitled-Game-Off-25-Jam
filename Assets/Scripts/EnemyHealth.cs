using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 30f;
    [SerializeField] float currentHP;
   
    private void Awake()
    {
        currentHP = healthPoints;
        
    }

    
    
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if( currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
