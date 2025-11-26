using UnityEngine;

public class PlayerHealth : MonoBehaviour
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

        if (currentHP <= 0)
        {
            Debug.Log("Player defeated");
        }
    }
}
