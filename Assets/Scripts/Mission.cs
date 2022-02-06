using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    ClearMonster,
    GetAllItem
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
            default:
                break;
        }
        return false;
    }
}
