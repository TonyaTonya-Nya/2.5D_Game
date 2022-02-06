using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public List<Enemy> enemies;

    private void Start()
    {
        foreach (Enemy enemy in enemies)
            enemy.OnDie += () => enemies.Remove(enemy);
    }

    public bool HasEnemy()
    {
        return enemies.Count != 0;
    }
}
