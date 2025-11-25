using UnityEngine;

public class RecycleBin : MonoBehaviour
{
    [SerializeField]
    public BinColor color;
    [SerializeField] Notification notification;
    private AudioManager audioManager;
    [SerializeField] EndGame endGameCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Trash trash = other.GetComponent<Trash>();

        if (trash != null)
        {
            //Debug.Log("Triggered by an object with EnemyScript: " + trash.trashtype);
            //Debug.Log("Triggered by an object with EnemyScript: " + trash.rigidBody.isKinematic);

            if (trash.rigidBody != null && !trash.rigidBody.isKinematic)
            {
                //Debug.Log("Triggered by an object : " + other.gameObject.name);
                //Debug.Log("Triggered by trashtype : " + trash.trashtype);

                if (trash.trashObject.GetTrashType().GetBin() == color)
                {
                    GlobalProperties.AddScore(trash.score);
                    bool firstFound = GlobalProperties.AddFoundTrash(trash.trashObject);
                    if (firstFound) {
                        audioManager.PlaySFX(audioManager.firstFind);
                        notification.Show(); 
                    }
                    else
                    {
                        audioManager.PlaySFX(audioManager.good);
                    }
                }
                else
                {
                    GlobalProperties.AddScore(-trash.score);
                    audioManager.PlaySFX(audioManager.bad);
                }
                GlobalProperties.remainingTrash--;
                if (GlobalProperties.remainingTrash <= 0)
                {
                    audioManager.PlaySFX(audioManager.endGame);
                    endGameCanvas.Open();
                } 
                Destroy(other.gameObject);
            }
        }

    }
}
