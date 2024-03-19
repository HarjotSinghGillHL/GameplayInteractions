using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_InteractableInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Interactor;
    public Camera Cam;

    public float PopupTime = 3.5f;
    public float TriggerMagnitude = 3.0f;
    public string InteractableText = "This is an interactable";

    Vector2 vecInteractableTextSize;
    GUIStyle DisplayTextStyle;

    private float flCurrentPopupTime = 0.0f;
    void Start()
    {
        flCurrentPopupTime = PopupTime;

 
    
    }

    // Update is called once per frame
    void OnGUI()
    {
        if (DisplayTextStyle == null)
        {
            DisplayTextStyle = new GUIStyle(GUI.skin.label);
            DisplayTextStyle.fontSize = 20;
            DisplayTextStyle.alignment = TextAnchor.MiddleCenter;
            GUIContent gUIContent = new GUIContent(InteractableText);
            vecInteractableTextSize = DisplayTextStyle.CalcSize(gUIContent);
        }
        float flDistance = Mathf.Abs((transform.position- Interactor.transform.position).magnitude);
       
        if (flDistance < TriggerMagnitude)
        {
            if (flCurrentPopupTime > 0.0f)
            {
                Vector2 vecScreenPosition = Cam.WorldToScreenPoint(transform.position);

                Rect rect_ = new Rect(vecScreenPosition.x, Screen.height - vecScreenPosition.y, vecInteractableTextSize.x, vecInteractableTextSize.y);
                GUI.Label(rect_, InteractableText, DisplayTextStyle);

                flCurrentPopupTime-=Time.deltaTime; 
            }
        }
        else
            flCurrentPopupTime = PopupTime;
    }
}
