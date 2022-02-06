using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public GameObject dialogueBox;
    public Text contentText;

    public Action OnDialogueEnd;

    private void Awake()
    {
        Instance = this;
    }

    public void StartDialogue(Vector3 objectPosition, List<string> dialogues)
    {
        dialogueBox.SetActive(true);
        Vector3 pos = Camera.main.WorldToScreenPoint(objectPosition + new Vector3(0, 7, 0));
        dialogueBox.transform.position = pos ;
        StartCoroutine(DialogueCoroutine(dialogues));
    }

    private IEnumerator DialogueCoroutine(List<string> dialogues)
    {
        foreach(string s in dialogues)
        {
            contentText.text = s;
            while (!Input.GetKeyDown(KeyCode.Space))
                yield return null;
            yield return null;
        }
        dialogueBox.SetActive(false);
        OnDialogueEnd?.Invoke();
    }
}
