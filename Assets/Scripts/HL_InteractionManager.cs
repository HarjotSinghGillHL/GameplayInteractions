using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class GameObjectExtensions
{
    /// <summary>
    /// Checks if a GameObject has been destroyed.
    /// </summary>
    /// <param name="gameObject">GameObject reference to check for destructedness</param>
    /// <returns>If the game object has been marked as destroyed by UnityEngine</returns>
    public static bool IsDestroyed(this GameObject gameObject)
    {
        // UnityEngine overloads the == opeator for the GameObject type
        // and returns null when the object has been destroyed, but 
        // actually the object is still there but has not been cleaned up yet
        // if we test both we can determine if the object has been destroyed.
        return gameObject == null && !ReferenceEquals(gameObject, null);
    }
}
public class HL_InteractionManager : MonoBehaviour
{
    public GameObject Interactor;
    public Camera InteractorCamera;

    public string InteractablesTag = "Interactable";
    public KeyCode InteractionKey = KeyCode.F;

    string KeyText = "Interaction Key Is ";

    GUIStyle DisplayTextStyle;
    Vector2 vecInteractableTextSize;

    private HL_KeyState KeyState;

    private List<GameObject> Interactables;

    public bool bQueueForDeletion = false;
    void Start()
    {
        Interactables = new List<GameObject>();
        KeyState = gameObject.GetComponent<HL_KeyState>();
    }
    public void PushInteractable(GameObject GameObject)
    {
        if (Interactables == null)
            Interactables = new List<GameObject>();

        Interactables.Add(GameObject);
    }
    public void RemoveInteractable(GameObject GameObject)
    {
        Interactables.Remove(GameObject);
    }

    public void OnPreSceneLoad()
    {
       Interactables.Clear();
    }

    void OnGUI()
    {
        if (DisplayTextStyle == null)
        {
            DisplayTextStyle = new GUIStyle(GUI.skin.label);
            DisplayTextStyle.fontSize = 20;
            DisplayTextStyle.alignment = TextAnchor.MiddleCenter;
            KeyText = "Interaction Key Is " + InteractionKey;
            GUIContent gUIContent = new GUIContent(KeyText);
            vecInteractableTextSize = DisplayTextStyle.CalcSize(gUIContent);
        }


    //  if (bQueueForDeletion)
    // {

    // return;
    //}

    LABEL_LOOP_AGAIN:

        if (Interactables != null && Interactables.Count > 0)
        {

            GameObject ClosestObject = null;
            HL_Interactable InteractableScript = null;
            float flLastMagnitude = 0.0f;

            foreach (GameObject obj in Interactables)
            {
                if (GameObjectExtensions.IsDestroyed(obj))
                {
                    Interactables.Remove(obj);
                    goto LABEL_LOOP_AGAIN;
                }
                  //  Interactables.Clear();

                float flDistance = Mathf.Abs((obj.transform.position - Interactor.transform.position).magnitude);

                if (ClosestObject == null || flDistance < flLastMagnitude)
                {
                    HL_Interactable _InteractableScript = obj.GetComponent<HL_Interactable>();
                    if (_InteractableScript != null)
                    {
                        ClosestObject = obj;
                        flLastMagnitude = flDistance;
                        InteractableScript = _InteractableScript;
                    }
                }
            }

            if (ClosestObject)
            {
                InteractableScript.HandleInteraction(InteractorCamera, flLastMagnitude);

                if (KeyState.CheckKeyState(InteractionKey, EKeyQueryMode.KEYQUERY_SINGLEPRESS))
                {
                    InteractableScript.OnInteractionKeyPress(InteractorCamera, flLastMagnitude);
                }


            }
        }


    }
}
