using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamager : MonoBehaviour
{
    GameObject boss=null;
    // Start is called before the first frame update
    void Start()
    {
       boss = GameObject.Find("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Damage()
    {
        if (boss != null)
        {
            boss.GetComponent<Enemy>().TakeDamage(50, 2);
        }
    }


}
