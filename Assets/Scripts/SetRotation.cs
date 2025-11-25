using UnityEngine;

public class SetRotation : MonoBehaviour
{
    private Transform parent;
    private Quaternion initialRotation;
    public float heightOffset = 10f;

    void Start()
    {
        parent = transform.parent;
        transform.SetParent(null);  
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (parent == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.rotation = initialRotation;
        transform.position = parent.position + Vector3.up * heightOffset;
    }
}
