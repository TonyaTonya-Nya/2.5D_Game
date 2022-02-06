using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    ClearMonster
}

public class Mission : MonoBehaviour
{
    public MissionType type;
    public Area area;

    public bool CheckComplete()
    {
        switch (type)
        {
            case MissionType.ClearMonster:
                return !area.HasEnemy();
            default:
                break;
        }
        return false;
    }
}
