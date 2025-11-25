using Oculus.Interaction;
using Oculus.Interaction.Input;
using System.Collections;
using UnityEngine;

public class TriggerHapticOnGrab : MonoBehaviour
{
    [Range(0f, 2.5f)]
    [SerializeField] public float duration = 0.1f;
    [Range(0f, 1f)]
    [SerializeField] public float amplitude = 0.7f;
    [Range(0f, 1f)]
    [SerializeField] public float frequency = 0.8f;

    private GrabInteractable grabInteractable;
    private DistanceGrabInteractable distanceGrabInteractable;

    private AudioManager audioManager;
    private Trash trash;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grabInteractable = GetComponent<GrabInteractable>();
        distanceGrabInteractable = GetComponent<DistanceGrabInteractable>();
        if(grabInteractable != null ) grabInteractable.WhenSelectingInteractorAdded.Action += WhenSelectingInteractorAdded_Action;
        if (grabInteractable != null) distanceGrabInteractable.WhenSelectingInteractorAdded.Action += WhenSelectingInteractorAdded_Action;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        trash = GetComponent<Trash>();

    }

    private void WhenSelectingInteractorAdded_Action(GrabInteractor obj)
    {
        HandleInteractor(obj.gameObject);
    }
    private void WhenSelectingInteractorAdded_Action(DistanceGrabInteractor obj)
    {
        HandleInteractor(obj.gameObject);
    }

    private void HandleInteractor(GameObject interactorGO)
    {
        ControllerRef controllerRef = interactorGO.GetComponent<ControllerRef>();
        if (controllerRef == null) return;

        TriggerHaptics(controllerRef.Handedness == Handedness.Left ?
            OVRInput.Controller.LTouch :
            OVRInput.Controller.RTouch);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerHaptics(OVRInput.Controller controller)
    {
        StartCoroutine(TriggerHapticsRoutine(controller));
    }

    public IEnumerator TriggerHapticsRoutine(OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}
