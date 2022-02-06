using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private TriggerManager tMag;
    public string objName;

    public Action OnHide;

    // Start is called before the first frame update
    void Start()
    {
        tMag = GameObject.FindGameObjectWithTag("TriggerManager").GetComponent<TriggerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tMag.ChangeStatus(objName, true);
            gameObject.SetActive(false);
            OnHide?.Invoke();
        }
    }

}
