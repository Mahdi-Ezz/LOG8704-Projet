using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] public GameObject leftHandDistanceGrabInteractor;
    [SerializeField] public GameObject leftHandGrabInteractor;
    [SerializeField] public GameObject rightHandDistanceGrabInteractor;
    [SerializeField] public GameObject rightHandGrabInteractor;
    [SerializeField] public GameObject HUD;

    private int currentPage;
    [SerializeField] public List<GameObject> pages;

    public OVRInput.Button nextButton;
    public OVRInput.Button closeButton;

    public bool isOpen = false;

    void Start()
    {
        Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen) { return; }
        if (OVRInput.GetDown(nextButton, OVRInput.Controller.RTouch))
        {
            nextPage();
        }
        if (OVRInput.Get(closeButton, OVRInput.Controller.LTouch))
        {
            Close();
        }
    }

    public void Open()
    {
        HUD.SetActive(false);
        isOpen = true;
        leftHandDistanceGrabInteractor.gameObject.SetActive(false);
        leftHandGrabInteractor.gameObject.SetActive(false);
        rightHandDistanceGrabInteractor.gameObject.SetActive(false);
        rightHandGrabInteractor.gameObject.SetActive(false);

        currentPage = 0;
        pages[0].SetActive(true);
    }

    public void Close()
    {
        HUD.SetActive(true);
        isOpen = false;
        leftHandDistanceGrabInteractor.gameObject.SetActive(true);
        leftHandGrabInteractor.gameObject.SetActive(true);
        rightHandDistanceGrabInteractor.gameObject.SetActive(true);
        rightHandGrabInteractor.gameObject.SetActive(true);

        pages[currentPage].SetActive(false);
        
    }

    public void nextPage()
    {
        pages[currentPage].SetActive(false);
        currentPage++;
        if (currentPage >= pages.Count) { 
            Close();
            return;
        }
        pages[currentPage].SetActive(true);
    }
}
