using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header ("Health Variables")]
    [SerializeField] float healthPoints = 5f;
    [SerializeField] float currentHP;
    [SerializeField] Image[] healthBars;
    
    [Header ("Damage Control")]
    public bool wasHit;
    [SerializeField] float hitDelay = 0.5f;

    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject gameOverUI;
    GameManager gameManager;
    PlayerController playerController;


    private void Awake()
    {
        currentHP = healthPoints;
        wasHit = false;
        AdjustHealthUI();
        gameManager = FindFirstObjectByType<GameManager>();
        playerController = GetComponent<PlayerController>();
    }



    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        wasHit = true;
        AdjustHealthUI();

        StartCoroutine(HitableRoutine(hitDelay));
        if (currentHP <= 0)
        {
            StartCoroutine(DeathRoutine());
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

    IEnumerator DeathRoutine()
    {
        playerController.enabled = false;
        Instantiate(deathVFX, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        gameOverUI.SetActive(true);
        Destroy(this.gameObject);

    }

}
