using System;
using UnityEngine;

public class RotateArrow : MonoBehaviour
{
    [SerializeField] Transform PlayerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation =  Quaternion.Euler(this.transform.rotation.eulerAngles.x, PlayerTransform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
    }
}
