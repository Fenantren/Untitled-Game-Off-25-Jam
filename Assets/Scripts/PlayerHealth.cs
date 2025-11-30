using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 30f;
    [SerializeField] float currentHP;
    public bool wasHit;

    [SerializeField] float hitDelay = 0.5f;

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
        if (currentHP <= 0)
        {
            Debug.Log("Player defeated");
        }
    }
    IEnumerator HitableRoutine(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        wasHit = false;
    }
}
