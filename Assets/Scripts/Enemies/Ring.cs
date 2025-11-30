using UnityEngine;

public class Ring : MonoBehaviour
{
    Transform ringTransform;
    [Header ("Ring Variables")]
    [SerializeField] float increaseRate = 5f;
    [SerializeField] float maxSize;
    

    [SerializeField] int damage = 2;
    
    [Header("SFX and VFX ")]
    [SerializeField] AudioSource damageSFXSource;
    [SerializeField] AudioClip damageSFXClip;
    [SerializeField] GameObject damageVFX;
    private void Awake()
    {
        ringTransform = GetComponent<Transform>();   
    }

    private void Update()
    {
        ringTransform.localScale += new Vector3(2,1,2) * increaseRate * Time.deltaTime;
        DestroyRing();
    }
    private void DestroyRing()
    {
        if (ringTransform.localScale.z >= maxSize)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth player = collision.collider.GetComponentInParent<PlayerHealth>();

        if (player.wasHit == false)
        {
            player?.TakeDamage(damage);
        
        
            damageSFXSource.PlayOneShot(damageSFXClip);
            Instantiate(damageVFX, collision.collider.transform.position, Quaternion.identity);
            
        }
        

    }
    /*public void OnTriggerEnter(Collider other)
    {
        Collider colliderHit = other.GetComponentInParent<Collider>();

        player?.TakeDamage(damage);

        //other.attachedRigidbody.AddForceAtPosition((this.GetComponent<MeshCollider>().bounds.extents - colliderHit.transform.position) * pushForce, colliderHit.transform.position, ForceMode.Impulse);
        damageSFXSource.PlayOneShot(damageSFXClip);
        Instantiate(damageVFX, colliderHit.transform.position, Quaternion.identity);

        //StartCoroutine(DelayedDestroyWaveRoutine());
    }*/
}
