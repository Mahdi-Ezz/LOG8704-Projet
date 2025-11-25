using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using static Meta.XR.MRUtilityKit.FindSpawnPositions;

public class Spawner : MonoBehaviour
{
    [SerializeField] public int numberOfTrash;


    private List<GameObject> spawnPoints;
    private List<GameObject> randomSpawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetAllSpawnPoints();
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetAllSpawnPoints()
    {
        spawnPoints = new List<GameObject>();

        foreach (Transform room in this.transform)
        {
            foreach (Transform spawnPoint in room)
            {
                spawnPoints.Add(spawnPoint.gameObject);
            }
        }
    }

    void Spawn()
    {
        randomSpawnPoints = GetRandomSpawnPoints();
        GlobalProperties.totalTrash = randomSpawnPoints.Count;
        GlobalProperties.remainingTrash = GlobalProperties.totalTrash;

        foreach (GameObject randomSpawnPoint in randomSpawnPoints)
        {
            SpawnProperties spawnProperties = randomSpawnPoint.GetComponent<SpawnProperties>();
            if(spawnProperties != null)
            {
                GameObject prefab = spawnProperties.GetRandomPrefab();
                GameObject trash = Instantiate(prefab, randomSpawnPoint.transform.position, prefab.transform.rotation);
                trash.transform.SetParent(randomSpawnPoint.transform);
            }

        }
    }

    public List<GameObject> GetRandomSpawnPoints()
    {
        List<GameObject> copy = new List<GameObject>(spawnPoints);

        for (int i = 0; i < copy.Count; i++)
        {
            int rand = Random.Range(i, copy.Count);
            (copy[i], copy[rand]) = (copy[rand], copy[i]);
        }

        // Take the first X
        return copy.GetRange(0, Mathf.Min(numberOfTrash, copy.Count));
    }

    public void Restart()
    {
        foreach (GameObject spawnPoint in randomSpawnPoints)
        {
            foreach (Transform child in spawnPoint.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        GetAllSpawnPoints();
        Spawn();
        GlobalProperties.Reset();
    }
}
