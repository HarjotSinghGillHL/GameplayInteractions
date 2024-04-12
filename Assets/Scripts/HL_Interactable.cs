using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HL_Interactable;

public class HL_Interactable : MonoBehaviour
{
    private GameObject InteractionManagerGameObject = null;
    HL_InteractionManager InteractionManager = null;
 
    private GameObject TaskManagerGameObject = null;
    HL_TaskManager TaskManager = null;

    private GameObject DialogueManagerGameObject = null;
    private HL_DialogueManager DialogueManager = null;
    public List<string> Dialogues;
    public List<string> PostDialogues;
    public bool bThisNpcGivesTask = false;

    public HL_TaskManager.ETaskType TaskType;
    public List<string> TaskFinishDialogues;

    public string DialogueOwner = "Some NPC";

    private GameObject Interactor;
    private HL_PlayerController InteractorController;

    public float PopupTime = 3.5f;
    public float TriggerMagnitude = 3.0f;
    public string InteractableText = "This is an interactable";
  
    public bool bTaskCompleted = false;
    private bool TaskStarted = false;

    Vector2 vecInteractableTextSize;
    GUIStyle DisplayTextStyle;

    private float flCurrentPopupTime = 0.0f;
    public enum EInteractableType
    {
        INTERACTABLE_SIGN = 0,
        INTERACTABLE_HEALTH_PICKUP,
        INTERACTABLE_SHIELD_PICKUP,
        INTERACTABLE_DIAMOND_PICKUP,
        INTERACTABLE_COIN_PICKUP,
        INTERACTABLE_DIALOGUE,
        INTERACTABLE_MAX
    }

    public EInteractableType InteractableType = EInteractableType.INTERACTABLE_SIGN;

    void Start()
    {
        InteractionManagerGameObject = GameObject.Find("InteractionManager");
        InteractionManager = InteractionManagerGameObject.GetComponent<HL_InteractionManager>();

        DialogueManagerGameObject = GameObject.Find("DialogueManager");
        DialogueManager = DialogueManagerGameObject.GetComponent<HL_DialogueManager>();
       
        TaskManagerGameObject = GameObject.Find("TaskManager");
        TaskManager = TaskManagerGameObject.GetComponent<HL_TaskManager>();

        InteractionManager.PushInteractable(gameObject);
        flCurrentPopupTime = PopupTime;

        Interactor = InteractionManager.Interactor;
        InteractorController = Interactor.GetComponent<HL_PlayerController>();
    }
    public void OnInteractionKeyPress(Camera Cam, float flMagnitude)
    {
        if (flMagnitude < TriggerMagnitude)
        {
            switch (InteractableType)
            {
                case EInteractableType.INTERACTABLE_SIGN:
                    {
                        break;
                    }
                case EInteractableType.INTERACTABLE_HEALTH_PICKUP:
                    {
                        if (InteractorController.Heal(50))
                        {
                            InteractionManager.RemoveInteractable(gameObject);
                            TaskManager.bHealed = true;
                            GameObject.Destroy(gameObject);
                        }
                        break;
                    }
                case EInteractableType.INTERACTABLE_SHIELD_PICKUP:
                    {
                        if (InteractorController.GainShield(50))
                        {
                            InteractionManager.RemoveInteractable(gameObject);
                            TaskManager.bShieldGained = true;
                            GameObject.Destroy(gameObject);
                        }
                        break;
                    }
                case EInteractableType.INTERACTABLE_DIAMOND_PICKUP:
                    {
                        InteractorController.GainScore(500);
                        InteractionManager.RemoveInteractable(gameObject);
                        TaskManager.bDiamondCollected = true;
                        GameObject.Destroy(gameObject);
                        break;
                    }
                case EInteractableType.INTERACTABLE_COIN_PICKUP:
                    {
                        InteractorController.GainScore(200);
                        InteractionManager.RemoveInteractable(gameObject);
                        GameObject.Destroy(gameObject);
                        break;
                    }
                case EInteractableType.INTERACTABLE_DIALOGUE:
                    {
                        if (bThisNpcGivesTask)
                        {
                            if (bTaskCompleted)
                            {
                                if (TaskFinishDialogues.Count > 0)
                                {
                                    DialogueManager.PushDialogues(TaskFinishDialogues, DialogueOwner);
                                }
                            }
                            else
                            {
                                if (!TaskStarted)
                                {
                                    if (!TaskManager.PushNewTaskOwner(this.gameObject,TaskType))
                                    {
                                        List<string> CompletePreviousTask = new List<string>();
                                        CompletePreviousTask.Add("Complete your previous task first");
                                        DialogueManager.PushDialogues(CompletePreviousTask, DialogueOwner);
                                        return;
                                    }

                                    TaskStarted = true;
                                }

                                if (Dialogues.Count > 0)
                                {
                                    DialogueManager.PushDialogues(Dialogues, DialogueOwner);
                                    Dialogues.Clear();
                                }
                                else if (PostDialogues.Count > 0)
                                {
                                    DialogueManager.PushDialogues(PostDialogues, DialogueOwner);
                                }
                            }
                        }
                        else
                        {
                            if (Dialogues.Count > 0)
                            {
                                DialogueManager.PushDialogues(Dialogues, DialogueOwner);
                                Dialogues.Clear();
                            }
                            else if (PostDialogues.Count > 0)
                            {
                                DialogueManager.PushDialogues(PostDialogues, DialogueOwner);
                            }
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
    public void HandleInteraction(Camera Cam, float flMagnitude)
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
