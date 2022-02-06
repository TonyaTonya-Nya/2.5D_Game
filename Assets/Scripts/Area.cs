using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public List<Enemy> enemies;
    public List<TriggerController> items;

    private void Start()
    {
        foreach (Enemy enemy in enemies)
            enemy.OnDie += () => enemies.Remove(enemy);
        foreach (TriggerController triggerController in items)
            triggerController.OnHide += () => items.Remove(triggerController);
    }

    public bool HasEnemy()
    {
        return enemies.Count != 0;
    }

    public bool HasItem()
    {
        return items.Count != 0;
    }
}
