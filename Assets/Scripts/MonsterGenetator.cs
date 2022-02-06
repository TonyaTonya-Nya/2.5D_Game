using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyGeneratorRatio
{
    public Enemy enemyPrefab;
    public int numberMin;
    public int numberMax;
}

public class MonsterGenetator : MonoBehaviour
{
    public List<EnemyGeneratorRatio> enemies;
    public List<Transform> generatePositions;
    // 出生的怪物也算在該區域內的話，出生時加進area的enemies
    public Area area;
    public int maxGenerateNumber;
    public float generateFrequency;

    private int nowNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Generate", 0, generateFrequency);
    }

    private void Generate()
    {
        if (nowNumber >= maxGenerateNumber)
            return;
        foreach (EnemyGeneratorRatio enemy in enemies)
        {
            int number = Random.Range(enemy.numberMin, enemy.numberMax + 1);
            Transform target = generatePositions[Random.Range(0, generatePositions.Count)];
            for (int i = 0;i < number;i++)
            {
                Enemy generatedEnemy = Instantiate(enemy.enemyPrefab, target.position, Quaternion.identity);
                if (area != null)
                    area.AddEnemy(generatedEnemy);
                nowNumber++;
                generatedEnemy.OnDie += () => nowNumber--;
                if (nowNumber >= maxGenerateNumber)
                    return;
            }
        }
    }
}
