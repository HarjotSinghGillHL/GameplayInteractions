using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

public class HL_DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject DialogueCanvas;
    public GameObject DialogueTextGameObject;
    public GameObject LocalPlayer;

    PlayerMovement_2D LocalPlayerMovement;

    UnityEngine.UI.Text DialgoueText;

    List<string> CurrentDialogues;
    public void PushDialogues(List<string> Dialogues)
    {
        if (CurrentDialogues.Count == 0)
        {
            LocalPlayerMovement.DisableMovement = true;
            LocalPlayerMovement.rb.velocity = Vector3.zero;

            CurrentDialogues.AddRange(Dialogues);

            if (CurrentDialogues.Count > 0)
            {
                DialogueCanvas.SetActive(true);
                DialgoueText.text = CurrentDialogues[0];
                CurrentDialogues.RemoveAt(0);
            }
        }
    }
    void Start()
    {
        CurrentDialogues = new List<string>();
        DialgoueText = DialogueTextGameObject.GetComponent<UnityEngine.UI.Text>();
        LocalPlayerMovement = LocalPlayer.GetComponent<PlayerMovement_2D>();
    }
    public void OnNextDialogue()
    {
        if (CurrentDialogues.Count > 0)
        {
            DialgoueText.text =CurrentDialogues[0];
            CurrentDialogues.RemoveAt(0);
        }
        else
        {
            DialogueCanvas.SetActive(false);
            LocalPlayerMovement.DisableMovement = false;
        }
   
    }
    void Update()
    {
        
    }
}
