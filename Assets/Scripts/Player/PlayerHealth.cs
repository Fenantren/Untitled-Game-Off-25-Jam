using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 5f;
    [SerializeField] float currentHP;
    public bool wasHit;

    [SerializeField] Image[] healthBars;
    [SerializeField] float hitDelay = 0.5f;

    private void Awake()
    {
        currentHP = healthPoints;
        wasHit = false;
        AdjustHealthUI();
    }



    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        wasHit = true;
        AdjustHealthUI();

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

    void AdjustHealthUI()
    {
        for (int i = 0; i < healthBars.Length; i++)
        {
            if (i < currentHP)
            {
                healthBars[i].gameObject.SetActive(true);
            }
            else
            {
                healthBars[i].gameObject.SetActive(false);

            }

        }
    }

}
