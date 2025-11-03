using UnityEngine;

public class Ring : MonoBehaviour
{
    Transform ringTransform;
    [SerializeField] float increaseRate = 5f;
    [SerializeField] float maxSize;
    private void Awake()
    {
        ringTransform = GetComponent<Transform>();   
    }

    private void Update()
    {
        ringTransform.localScale += new Vector3(1,0,1) * increaseRate * Time.deltaTime;
        DestroyRing();
    }
    private void DestroyRing()
    {
        if (ringTransform.localScale.z >= maxSize)
        {
            Destroy(this.gameObject);
        }
    }
}
