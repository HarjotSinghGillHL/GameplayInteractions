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

    public GameObject GameManagerObject;
    HL_GameManager GameManger;

    HL_PlayerController LocalPlayerMovement;

    UnityEngine.UI.Text DialgoueText;
    public UnityEngine.UI.Text DialogueOwnerText;

    List<string> CurrentDialogues;

    int iCurrentDialogue = 0;
    public void PushDialogues(List<string> Dialogues,string DialogueOwner)
    {
        DialogueOwnerText.text = DialogueOwner;

        if (CurrentDialogues.Count == 0)
        {
            GameManger.bOverrideCursor = true;
            LocalPlayerMovement.DisableMovement = true;
            LocalPlayerMovement.rb.velocity = Vector3.zero;

            CurrentDialogues.AddRange(Dialogues);

            if (CurrentDialogues.Count > 0)
            {
                DialogueCanvas.SetActive(true);
                iCurrentDialogue = CurrentDialogues.Count;
                DialgoueText.text = CurrentDialogues[CurrentDialogues.Count - iCurrentDialogue];
                iCurrentDialogue -= 1;

            }
        }
    }
    void Start()
    {
        CurrentDialogues = new List<string>();
        DialgoueText = DialogueTextGameObject.GetComponent<UnityEngine.UI.Text>();
        GameManger = GameManagerObject.GetComponent<HL_GameManager>();
        LocalPlayerMovement = LocalPlayer.GetComponent<HL_PlayerController>();
    }
    public void OnNextDialogue()
    {
        if (iCurrentDialogue > 0)
        {
            DialgoueText.text =CurrentDialogues[CurrentDialogues.Count - iCurrentDialogue ];
            iCurrentDialogue -= 1;
        }
        else
        {
            CurrentDialogues.Clear();
            GameManger.bOverrideCursor = false;
            DialogueCanvas.SetActive(false);
            LocalPlayerMovement.DisableMovement = false;
        }
   
    }
    void Update()
    {
        
    }
}
