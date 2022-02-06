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

    public void StartDialogue()
    {
        if (isCompleteMission)
        {
            DialogueManager.Instance.StartDialogue(transform.position, postDialogue);
        }
        else if (isInMission)
        {
            if (mission.CheckComplete())
            {
                DialogueManager.Instance.OnDialogueEnd += FinishMission;
                DialogueManager.Instance.StartDialogue(transform.position, postDialogue);
            }
            else
                DialogueManager.Instance.StartDialogue(transform.position, inDialogue);
        }
        else
        {
            if (mission != null)
                DialogueManager.Instance.OnDialogueEnd += StartMission;
            DialogueManager.Instance.StartDialogue(transform.position, preDialogue);
        }
    }

    private void StartMission()
    {
        isInMission = true;
        DialogueManager.Instance.OnDialogueEnd -= StartMission;
    }

    private void FinishMission()
    {
        isCompleteMission = true;
        isInMission = false;
        TriggerManager.Instance.ChangeStatus(doorName, true);
        DialogueManager.Instance.OnDialogueEnd -= FinishMission;
    }
}
