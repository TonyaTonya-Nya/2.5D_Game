using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EventTrigger
{
    public string name;
    public bool status;
}

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager Instance { get; private set; }

    public EventTrigger[] trigger;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ChangeStatus(string str, bool b)
    {
        for (int i = 0; i < trigger.Length; i++)
        {
            if (trigger[i].name == str)
            {
                trigger[i].status = b;
                break;
            }
        }
    }


}
