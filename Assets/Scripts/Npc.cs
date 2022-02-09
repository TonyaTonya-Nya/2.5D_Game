using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public List<string> preDialogue;
    public List<string> inDialogue;
    public List<string> postDialogue;

    public string doorName;

    public Mission mission;

    private bool isInMission;
    private bool isCompleteMission;

    public DialogueManager dialogueManager;


    public bool canDialogue = false;
    private bool isE = false;
    private float deltaT = 0;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            isE = true;
        }

        if (isE)
        {
            deltaT += Time.deltaTime;
        }

        if (deltaT > 0.5f)
        {
            deltaT = 0;
            isE = false;
        }
    }


    public void StartDialogue()
    {
        if (isCompleteMission)
        {
            dialogueManager.StartDialogue(transform.position, postDialogue);
        }
        else if (isInMission)
        {
            if (mission.CheckComplete())
            {
                dialogueManager.OnDialogueEnd += FinishMission;
                dialogueManager.StartDialogue(transform.position, postDialogue);
            }
            else
                dialogueManager.StartDialogue(transform.position, inDialogue);
        }
        else
        {
            if (mission != null)
                dialogueManager.OnDialogueEnd += StartMission;
            dialogueManager.StartDialogue(transform.position, preDialogue);
        }
    }

    private void StartMission()
    {
        isInMission = true;
        dialogueManager.OnDialogueEnd -= StartMission;
    }

    private void FinishMission()
    {
        isCompleteMission = true;
        isInMission = false;
        TriggerManager.Instance.ChangeStatus(doorName, true);
        dialogueManager.OnDialogueEnd -= FinishMission;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            canDialogue = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            canDialogue = false;
    }

    public bool DialogueCheck()
    {
        return canDialogue & isE;
    }
}
