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
    int iDialogueIndex = 0;
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
        if (iDialogueIndex < CurrentDialogues.Count)
        {
            DialgoueText.text =CurrentDialogues[iDialogueIndex];
            iDialogueIndex += 1;
           // CurrentDialogues.RemoveAt(0);
        }
        else
        {
            iDialogueIndex = 0;
            CurrentDialogues.Clear();
            DialogueCanvas.SetActive(false);
            LocalPlayerMovement.DisableMovement = false;
        }
   
    }
    void Update()
    {
        
    }
}
