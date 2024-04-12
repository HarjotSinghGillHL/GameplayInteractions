using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HL_TaskManager : MonoBehaviour
{
    public TMP_Text ObjectiveText;

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
    public enum ETaskType
    {
        TASK_NONE = 0,
        TASK_COLLECT_A_DIAMOND,
        TASK_HEAL_YOURSELF,
        TASK_GAIN_SHIELD,
        TASK_COLLECT_A_KEY,
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
            case ETaskType.TASK_COLLECT_A_KEY:
                {
                    ObjectiveText.text = "Collect a key";
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
            default:
                break;
        }
  
    }
}
