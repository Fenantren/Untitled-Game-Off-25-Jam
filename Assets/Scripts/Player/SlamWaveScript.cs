using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SlamWaveScript : MonoBehaviour
{
    [Header ("Variables")]
    [SerializeField] float increaseRate = 2f;
    [SerializeField] float maxSize;
    [SerializeField] int damage = 2;
    [SerializeField] float waveFrequency = 3f;
    
    [Header ("VFX and SFX")]
    [SerializeField] GameObject damageVFX;

    [SerializeField] AudioSource damageSFXSource;
    [SerializeField] AudioClip damageSFX;


    
    private void Update()
    {
        
        WaveExpansion();
    }
    void WaveExpansion()
    {

        transform.localScale += new Vector3(0.75f, 0.4f *  Mathf.Sin(waveFrequency * Time.time), 0.75f) * increaseRate * Time.deltaTime ;

        if(transform.localScale.z >= maxSize)
        {
            Destroy(this.gameObject);
        }
    }

    /*public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        EnemyHealth enemyHealth = collision.collider.GetComponentInParent<EnemyHealth>();

        if (enemyHealth.wasHit == false)
        {
            enemyHealth?.TakeDamage(damage);

        }
        else return;






            Debug.Log("Enemy took damage");
        //Play SFX and VFX

        StartCoroutine(DelayedDestroyWaveRoutine());
    }*/


    public void OnTriggerEnter(Collider other)
    {

        EnemyHealth enemyHealth = other.GetComponent<Collider>().GetComponentInParent<EnemyHealth>();
        Collider colliderHit = other.GetComponentInParent<Collider>(); 
        if (enemyHealth.wasHit == false)
        {
            enemyHealth?.TakeDamage(damage);
            Instantiate(damageVFX, colliderHit.transform.position, Quaternion.identity);
            damageSFXSource.PlayOneShot(damageSFX);
        }
        else return;


       

        StartCoroutine(DelayedDestroyWaveRoutine());
    }
    IEnumerator DelayedDestroyWaveRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        
        Destroy(this.gameObject);
    }
}
