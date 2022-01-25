using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    public bool attackCheck = false;
    public bool comboCheck = false;


    public void CheckInit()
    {
        AttackInit();
        ComboInit();
    }

    public void ComboInit()
    {
        comboCheck = true;
    }

    public void AttackInit()
    {
        attackCheck = true;
    }

    public void ComboEnd()
    {
        comboCheck = false;
    }

    public void AttackEnd()
    {
        attackCheck = false;
    }

    public void CheckEnd()
    {
        ComboEnd();
        AttackEnd();
    }


}
