using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_InteractionManager : MonoBehaviour
{
    public GameObject Interactor;
    public Camera InteractorCamera;

    public string InteractablesTag = "Interactable";
    public KeyCode InteractionKey = KeyCode.F;

    private HL_KeyState KeyState;

    private List<GameObject> Interactables;
    void Start()
    {
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
    void OnGUI()
    {
        if (Interactables != null && Interactables.Count > 0)
        {
            GameObject ClosestObject = null;
            HL_Interactable InteractableScript = null;
            float flLastMagnitude = 0.0f;

            foreach (GameObject obj in Interactables)
            {
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
               // if (KeyState.CheckKeyState(InteractionKey, EKeyQueryMode.KEYQUERY_SINGLEPRESS))
                //{
                    InteractableScript.HandleInteraction(InteractorCamera, flLastMagnitude);
              //  }

            }
        }
    }
}
