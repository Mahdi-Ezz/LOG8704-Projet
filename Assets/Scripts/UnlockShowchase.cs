using UnityEngine;

public class UnlockShowchase : MonoBehaviour
{
    [SerializeField] public TrashObject trashObject;
    [SerializeField] public GameObject unlockedPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalProperties.foundTrash[trashObject])
        {
            Destroy(gameObject);
            Instantiate(unlockedPrefab, transform.position, transform.rotation);
        }
    }
}
