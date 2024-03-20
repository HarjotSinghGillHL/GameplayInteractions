using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Interactable : MonoBehaviour
{
    public GameObject InteractionManagerGameObject = null;
    HL_InteractionManager InteractionManager = null;

    public float PopupTime = 3.5f;
    public float TriggerMagnitude = 3.0f;
    public string InteractableText = "This is an interactable";

    Vector2 vecInteractableTextSize;
    GUIStyle DisplayTextStyle;

    private float flCurrentPopupTime = 0.0f;
    public enum EInteractableType
    {
        INTERACTABLE_SIGN = 0,
        INTERACTABLE_PICKUP,
        INTERACTABLE_MAX
    }

    public EInteractableType InteractableType = EInteractableType.INTERACTABLE_SIGN;
    
    void Start()
    {
        InteractionManager = InteractionManagerGameObject.GetComponent<HL_InteractionManager>();
        InteractionManager.PushInteractable(gameObject);
        flCurrentPopupTime = PopupTime;
    }

    public void HandleInteraction(Camera Cam,float flMagnitude)
    {
        if (DisplayTextStyle == null)
        {
            DisplayTextStyle = new GUIStyle(GUI.skin.label);
            DisplayTextStyle.fontSize = 20;
            DisplayTextStyle.alignment = TextAnchor.MiddleCenter;
            GUIContent gUIContent = new GUIContent(InteractableText);
            vecInteractableTextSize = DisplayTextStyle.CalcSize(gUIContent);
        }

        if (flMagnitude < TriggerMagnitude)
        {
            if (flCurrentPopupTime > 0.0f)
            {
                Vector2 vecScreenPosition = Cam.WorldToScreenPoint(transform.position);
                Rect rect_ = new Rect(vecScreenPosition.x, Screen.height - vecScreenPosition.y, vecInteractableTextSize.x, vecInteractableTextSize.y);
                GUI.Label(rect_, InteractableText, DisplayTextStyle);

                flCurrentPopupTime -= Time.deltaTime;
            }
        }
        else
            flCurrentPopupTime = PopupTime;


    }
}
