using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Interactable : MonoBehaviour
{
    public GameObject InteractionManagerGameObject = null;
    HL_InteractionManager InteractionManager = null;
    public enum EInteractableType
    {
        INTERACTABLE_SIGN = 0,
        INTERACTABLE_DIAMOND,
        INTERACTABLE_MAX
    }

    public EInteractableType InteractableType = EInteractableType.INTERACTABLE_SIGN;
    
    void Start()
    {
        InteractionManager = InteractionManagerGameObject.GetComponent<HL_InteractionManager>();
        InteractionManager.PushInteractable(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleInteraction(float flMagnitude)
    {
        Debug.Log("Trying to Interact with object : " + gameObject.name + " Type : " + InteractableType + " Magnitude : " + flMagnitude);

    }
}
