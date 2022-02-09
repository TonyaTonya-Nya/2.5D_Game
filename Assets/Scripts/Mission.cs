using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    ClearMonster,
    GetAllItem,
    None
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
            case MissionType.GetAllItem:
                return !area.HasItem();
            case MissionType.None:
                return true;
            default:
                break;
        }
        return false;
    }
}
