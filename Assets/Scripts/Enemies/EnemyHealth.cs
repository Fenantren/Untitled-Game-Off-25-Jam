using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header ("Health Variables")]
    [SerializeField] float healthPoints = 30f;
    [SerializeField] float currentHP;
    [Header ("Damage control")]
    public bool wasHit;
    [SerializeField] float hitDelay =  0.7f;

    [Header ("References")]
    [SerializeField] GameObject destroyVFX;
    GameManager gameManager;

    private void Awake()
    {
        currentHP = healthPoints;
        wasHit = false;
    }
    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.AdjustEnemiesLeft(1);
    }


    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        wasHit = true;

        StartCoroutine(HitableRoutine(hitDelay));
        if( currentHP <= 0)
        {
            gameManager.AdjustEnemiesLeft(-1);
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator HitableRoutine(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        wasHit = false;
    }
}
