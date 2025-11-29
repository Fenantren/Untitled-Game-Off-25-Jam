using System.Collections;
using UnityEngine;

public class EnemyWaveScript : MonoBehaviour
{
    [SerializeField] float increaseRate = 2f;
    [SerializeField] float maxSize;
    [SerializeField] int damage = 2;
    [SerializeField] float waveFrequency = 7f;


    [Header("SFX and VFX ")]
    [SerializeField] AudioSource damageSFXSource;
    [SerializeField] AudioClip damageSFXClip;
    [SerializeField] GameObject damageVFX;
    
    
    PlayerController playerControllerScript;

    private void Awake()
    {
        playerControllerScript = FindFirstObjectByType<PlayerController>();
    }
    private void Update()
    {
        
        WaveExpansion();
    }
    void WaveExpansion()
    {
        transform.localScale += new Vector3(1, 0.4f * Mathf.Sin(waveFrequency * Time.time), 1) * increaseRate * Time.deltaTime;

        if (transform.localScale.z >= maxSize)
        {
            Destroy(this.gameObject);
        }
    }

    

    public void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<Collider>().GetComponentInParent<PlayerHealth>();
        Collider colliderHit = other.GetComponentInParent<Collider>();

        player?.TakeDamage(damage);
        damageSFXSource.PlayOneShot(damageSFXClip);
        Instantiate(damageVFX, colliderHit.transform.position, Quaternion.identity);

        StartCoroutine(DelayedDestroyWaveRoutine());
    }

    IEnumerator DelayedDestroyWaveRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(this.gameObject);
    }


}
