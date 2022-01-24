using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    public bool attackCheck = false;
    public bool comboCheck = false;


    public void CheckInit()
    {
        attackCheck = true;
    }

    public void ComboInit()
    {
        comboCheck = true;
    }

    public void CheckEnd()
    {
        attackCheck = false;
        comboCheck = false;
    }


}
