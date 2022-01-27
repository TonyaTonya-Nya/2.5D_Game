using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private GameObject door;
    private TriggerManager tMag;
    private int flagNum;

    public string objName;

    public GameObject leftDoor;
    public GameObject rightDoor;

    // Start is called before the first frame update
    void Start()
    {
        door = gameObject;
        tMag= GameObject.FindGameObjectWithTag("TriggerManager").GetComponent<TriggerManager>();

        for (int i = 0; i < tMag.trigger.Length; i++)
        {
            if (tMag.trigger[i].name == objName)
            {
                flagNum = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tMag.trigger[flagNum].status)
        {
            if (leftDoor.activeSelf || rightDoor.activeSelf)
            {
                leftDoor.SetActive(false);
                rightDoor.SetActive(false);
            }
        }

    }
}
