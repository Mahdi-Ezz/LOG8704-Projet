using Oculus.Interaction;
using Oculus.Interaction.Locomotion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public bool isOpen = false;
    [SerializeField] Spawner spawner;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] Transform startPosition;
    [SerializeField, Interface(typeof(ILocomotionEventHandler))]
    private List<UnityEngine.Object> _handlers;
    [SerializeField] public GameObject HUD;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        Close();
        GlobalProperties.isInMuseum = false;
        spawner.Restart();
        Pose pose = new Pose();
        pose.rotation = startPosition.rotation;
        pose.position = startPosition.position;
        LocomotionEvent locomotionEvent = new LocomotionEvent(0, pose, LocomotionEvent.TranslationType.Absolute, LocomotionEvent.RotationType.Absolute);
        List<ILocomotionEventHandler> Handlers = _handlers.ConvertAll(b => b as ILocomotionEventHandler);
        Handlers[0].HandleLocomotionEvent(locomotionEvent);
    }

    public void Open()
    {
        HUD.SetActive(false);
        textMeshPro.text = "Vous avez obtenu un score de " + GlobalProperties.currentScore;
        isOpen = true;
        gameObject.SetActive(true);
    }
    public void Close()
    {
        isOpen = false;
        gameObject.SetActive(false);
        HUD.SetActive(true);
    }
}
