using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HL_TaskManager : MonoBehaviour
{
    public TMP_Text ObjectiveText;

    public GameObject LocalPlayer;
    private HL_PlayerController LocalController;

    private GameObject TaskOwner;
    private HL_Interactable TaskOwnerInteractable;

    [HideInInspector]
    public bool bDiamondCollected = false;

    [HideInInspector]
    public bool bCoinCollected = false;

    [HideInInspector]
    public bool bHealed = false;

    [HideInInspector]
    public bool bShieldGained = false;

    [HideInInspector]
    public bool bKeyCaptured = false;

    [HideInInspector]
    public bool bHasNextLevelKey = false;

    [HideInInspector]
    public bool bReadSignBoard = false;
    public enum ETaskType
    {
        TASK_NONE = 0,
        TASK_COLLECT_A_DIAMOND,
        TASK_HEAL_YOURSELF,
        TASK_GAIN_SHIELD,
        TASK_COLLECT_A_LEVEL_KEY,
        TASK_READ_A_SIGNBOARD,
        TASK_MAX,
    }
   
    [HideInInspector]
    public ETaskType CurrentTask = ETaskType.TASK_NONE;
    public bool PushNewTaskOwner(GameObject _TaskOwner, ETaskType _NewTask)
    {
        if (CurrentTask != ETaskType.TASK_NONE)
            return false;

        TaskOwner = _TaskOwner;
        TaskOwnerInteractable = TaskOwner.GetComponent<HL_Interactable>();
        CurrentTask = _NewTask;

        switch (CurrentTask)
        {
            case ETaskType.TASK_READ_A_SIGNBOARD:
                {
                    ObjectiveText.text = "Read a signboard";
                    break;
                }
            case ETaskType.TASK_COLLECT_A_DIAMOND:
                {
                    ObjectiveText.text = "Collect a diamond";
                    break;
                }
            case ETaskType.TASK_HEAL_YOURSELF:
                {
                    ObjectiveText.text = "Collect a health pickup";
                    break;
                }
            case ETaskType.TASK_GAIN_SHIELD:
                {
                    ObjectiveText.text = "Collect a shield pickup";
                    break;
                }
            case ETaskType.TASK_COLLECT_A_LEVEL_KEY:
                {
                    ObjectiveText.text = "Collect a level key";
                    break;
                }
            default:
                {
                    ObjectiveText.text = "[Error] Unknown Objective";
                }
                break;
        }

        return true;
    }
    public void OnTaskCompleted()
    {
        if (TaskOwner != null)
        {
            TaskOwner = null;
            TaskOwnerInteractable.bTaskCompleted = true;
            TaskOwnerInteractable = null;
            CurrentTask = ETaskType.TASK_NONE;
            ObjectiveText.text = "None";
        }
    }
    void Start()
    {
        LocalController = LocalPlayer.GetComponent<HL_PlayerController>();
    }

    void Update()
    {
        switch (CurrentTask)
        {
            case ETaskType.TASK_COLLECT_A_DIAMOND:
                {
                    if (bDiamondCollected)
                    {
                        OnTaskCompleted();
                    }
                   break;
                }
            case ETaskType.TASK_HEAL_YOURSELF:
                {
                    if (bHealed)
                    {
                        OnTaskCompleted();
                    }
                    break;
                }
            case ETaskType.TASK_GAIN_SHIELD:
                {
                    if (bShieldGained)
                    {
                        OnTaskCompleted();
                    }
                    break;
                }
            case ETaskType.TASK_COLLECT_A_LEVEL_KEY:
                {
                    if (bHasNextLevelKey)
                    {

                        OnTaskCompleted();
                    }
                    break;
                }
            case ETaskType.TASK_READ_A_SIGNBOARD:
                {
                    if (bReadSignBoard)
                    {
                        OnTaskCompleted();
                    }
                    break;
                }
            default:
                break;
        }
  
    }
}
