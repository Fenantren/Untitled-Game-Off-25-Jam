using UnityEngine;

public class Ring : MonoBehaviour
{
    Transform ringTransform;
    [SerializeField] float increaseRate = 5f;
    [SerializeField] float maxSize;
    [SerializeField] float pushForce = 20f;
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

    private void OnCollisionEnter(Collision other)
    {
       other.rigidbody.AddForce((other.transform.position - ringTransform.position) * pushForce, ForceMode.Impulse);
       //Add SFX and VFX
       
    }
}
