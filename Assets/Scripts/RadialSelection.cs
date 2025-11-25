using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Oculus.Interaction.Locomotion;
using Oculus.Interaction;
using Unity.VisualScripting;


public class RadialSelection : MonoBehaviour
{
    [SerializeField] public int numberOfRadialPart;
    [SerializeField] public GameObject radialPrefab;
    [SerializeField] public Transform radialPartCanvas;
    [SerializeField] public float angleBetweenPart = 10;
    [SerializeField] public Transform handTransform;
    [SerializeField] public GameObject arcVisual;
    [SerializeField] public GameObject locomotor;

    [SerializeField] public GameObject muteImage;
    [SerializeField] public GameObject unmuteImage;
    [SerializeField] public GameObject easyImage;
    [SerializeField] public GameObject hardImage;

    [SerializeField] public Transform startPosition;
    [SerializeField] public Transform teleportTransform;
    [SerializeField] public Reset resetCanvas;
    [SerializeField] public Instructions instructionsCanvas;
    [SerializeField] public GameObject leftHandDistanceGrabInteractor;
    [SerializeField] public GameObject leftHandGrabInteractor;
    [SerializeField] public GameObject rightHandDistanceGrabInteractor;
    [SerializeField] public GameObject rightHandGrabInteractor;
    Vector3 previousPos;
    Quaternion previousRot;

    [SerializeField] public Camera minimapCamera;

    [SerializeField, Interface(typeof(ILocomotionEventHandler))]
    private List<UnityEngine.Object> _handlers;

    public UnityEvent<int> onPartSelected;
    public OVRInput.Button spawnButton;

    public List<GameObject> spawnParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;
    private AudioManager audioManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (instructionsCanvas.isOpen || resetCanvas.isOpen) { return; }

        if (OVRInput.GetDown(spawnButton, OVRInput.Controller.RTouch))
        {
            SpawnRadialPart();
        }
        if (OVRInput.Get(spawnButton, OVRInput.Controller.RTouch))
        {
            GetSelectedRadialPart();
        }
        if (OVRInput.GetUp(spawnButton, OVRInput.Controller.RTouch))
        {
            HideAndTriggerSelected();
        }
    }

    public void SpawnRadialPart()
    {
        radialPartCanvas.gameObject.SetActive(true);
        radialPartCanvas.position = handTransform.position;
        radialPartCanvas.rotation = handTransform.rotation;
        locomotor.SetActive(false);
        arcVisual.SetActive(false);
        muteImage.SetActive(!audioManager.mute);
        unmuteImage.SetActive(audioManager.mute);
        easyImage.SetActive(GlobalProperties.isEasy);
        hardImage.SetActive(!GlobalProperties.isEasy);
        leftHandDistanceGrabInteractor.gameObject.SetActive(false);
        leftHandGrabInteractor.gameObject.SetActive(false);
        rightHandDistanceGrabInteractor.gameObject.SetActive(false);
        rightHandGrabInteractor.gameObject.SetActive(false);

    }

    public void GetSelectedRadialPart()
    {
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);
        RectTransform rt = spawnParts[spawnParts.Count - 1].GetComponent<RectTransform>();
        float innerRadius = rt.rect.width * radialPartCanvas.lossyScale.x / 2f;
        if (centerToHandProjected.magnitude < innerRadius)
        {
            currentSelectedRadialPart = spawnParts.Count - 1;
        }
        else
        {
            float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward);
            if (angle < 0) angle += 360;
            currentSelectedRadialPart = (int)(angle * numberOfRadialPart / 360);
        }
        for (int i = 0; i < spawnParts.Count; i++)
        {
            if (i == currentSelectedRadialPart)
            {
                spawnParts[i].GetComponent<Image>().color = Color.yellow;
                spawnParts[i].transform.localScale = Vector3.one * 1.1f;
            }
            else
            {
                spawnParts[i].GetComponent<Image>().color = Color.white;
                spawnParts[i].transform.localScale = Vector3.one;
            }
        }
    }

    public void HideAndTriggerSelected()
    {
        radialPartCanvas.gameObject.SetActive(false);
        locomotor.SetActive(true);
        arcVisual.SetActive(true);

        switch (currentSelectedRadialPart)
        {
            case 0:
                {
                    resetCanvas.Open();
                    break;
                }
            case 1:
                {
                    if (!GlobalProperties.isInMuseum)
                    {
                        Pose pose = new Pose();
                        pose.rotation = teleportTransform.rotation;
                        pose.position = teleportTransform.position;
                        previousPos = startPosition.position;
                        previousRot = startPosition.rotation;
                        LocomotionEvent locomotionEvent = new LocomotionEvent(0, pose, LocomotionEvent.TranslationType.Absolute, LocomotionEvent.RotationType.Absolute);
                        List<ILocomotionEventHandler> Handlers = _handlers.ConvertAll(b => b as ILocomotionEventHandler);
                        Handlers[0].HandleLocomotionEvent(locomotionEvent);
                    }
                    else
                    {
                        Pose pose = new Pose();
                        pose.rotation = previousRot;
                        pose.position = previousPos;
                        LocomotionEvent locomotionEvent = new LocomotionEvent(0, pose, LocomotionEvent.TranslationType.Absolute, LocomotionEvent.RotationType.Absolute);
                        List<ILocomotionEventHandler> Handlers = _handlers.ConvertAll(b => b as ILocomotionEventHandler);
                        Handlers[0].HandleLocomotionEvent(locomotionEvent);
                    }
                    GlobalProperties.isInMuseum = !GlobalProperties.isInMuseum;

                    break;
                }
            case 2:
                {
                    GlobalProperties.ChangeDifficulty();
                    int layerToModify = LayerMask.NameToLayer("Easy");
                    if (GlobalProperties.isEasy)
                    {
                        minimapCamera.cullingMask |= (1 << layerToModify);
                    }
                    else
                    {
                        minimapCamera.cullingMask &= ~(1 << layerToModify);
                    }
                    break;
                }
            case 3:
                {
                    instructionsCanvas.Open();
                    break;
                }
            case 4:
                {
                    audioManager.ToggleMute();
                    break;
                }

            case 5:
                {
                    break;
                }
        }
        leftHandDistanceGrabInteractor.gameObject.SetActive(true);
        leftHandGrabInteractor.gameObject.SetActive(true);
        rightHandDistanceGrabInteractor.gameObject.SetActive(true);
        rightHandGrabInteractor.gameObject.SetActive(true);
    }
}