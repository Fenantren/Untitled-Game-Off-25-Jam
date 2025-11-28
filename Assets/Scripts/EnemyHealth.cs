using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 30f;
    [SerializeField] float currentHP;
    public bool wasHit;
    [SerializeField] float hitDelay =  0.7f;
    private void Awake()
    {
        currentHP = healthPoints;
        wasHit = false;
    }

    
    
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        wasHit = true;

        StartCoroutine(HitableRoutine(hitDelay));
        if( currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator HitableRoutine(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        wasHit = false;
    }
}
