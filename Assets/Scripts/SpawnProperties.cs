using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnProperties : MonoBehaviour
{
    [SerializeField] public GameObject[] allowedTrash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetRandomPrefab()
    {
        return allowedTrash[Random.Range(0, allowedTrash.Length)];

    }
}
