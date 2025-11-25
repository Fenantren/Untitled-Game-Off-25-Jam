using UnityEngine;

public class SlamWaveScript : MonoBehaviour
{
    [SerializeField] float increaseRate = 2f;
    [SerializeField] float maxSize;
    [SerializeField] int damage = 2;

    private void Update()
    {
        WaveExpansion();
    }
    void WaveExpansion()
    {
        transform.localScale += new Vector3(1, 0, 1) * increaseRate * Time.deltaTime;

        if(transform.localScale.z >= maxSize)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        EnemyHealth enemyHealth = collision.collider.GetComponentInParent<EnemyHealth>();

        enemyHealth?.TakeDamage(damage);

        Debug.Log("Enemy took damage");
        //Play SFX and VFX

        Destroy(this.gameObject);
    }
}
