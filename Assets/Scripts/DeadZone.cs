using System.Data;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Trash trash = other.GetComponent<Trash>();

        if (trash != null )
        {
            if (trash.rigidBody != null && !trash.rigidBody.isKinematic)
            {
                GameObject parent = other.gameObject.transform.parent.gameObject;
                other.gameObject.transform.position = parent.transform.position;
            }
        }

    }
}
