using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    private Dictionary<Transform, int> generatePositionMaxNumber = new Dictionary<Transform, int>();
    private Dictionary<Transform, int> generatePositionNowNumber = new Dictionary<Transform, int>();

    // 出生的怪物也算在該區域內的話，出生時加進area的enemies
    public Area area;
    public int maxGenerateNumber;
    public float generateFrequency;

    private int nowNumber = 0;
    private Enemy selfEnemy;

    // Start is called before the first frame update
    void Start()
    {
        // 計算每個點平均可以生幾隻怪
        int average = maxGenerateNumber / generatePositions.Count;
        foreach (Transform position in generatePositions)
        {
            generatePositionMaxNumber[position] = average;
            generatePositionNowNumber[position] = 0;
        }
        // 剩餘部分隨機分配
        int remain = maxGenerateNumber - generatePositions.Count * average;
        List<Transform> gs = new List<Transform>(generatePositions);
        for (int i = 0; i < remain; i++)
        {
            Transform t = gs[Random.Range(0, gs.Count)];
            gs.Remove(t);
            generatePositionMaxNumber[t] += 1;
            generatePositionNowNumber[t] = 0;
        }

        InvokeRepeating("Generate", 0, generateFrequency);
        selfEnemy = GetComponent<Enemy>();
        selfEnemy.OnDie += () => CancelInvoke("Generate");
    }

    private void Reallocate()
    {
        // 計算每個點平均可以生幾隻怪
        int average = maxGenerateNumber / generatePositions.Count;
        foreach (Transform position in generatePositions)
        {
            generatePositionMaxNumber[position] = average;
        }
        // 剩餘部分隨機分配
        int remain = maxGenerateNumber - generatePositions.Count * average;
        List<Transform> gs = new List<Transform>(generatePositions);
        for (int i = 0; i < remain; i++)
        {
            Transform t = gs[Random.Range(0, gs.Count)];
            gs.Remove(t);
            generatePositionMaxNumber[t] += 1;
        }
    }

    private void Generate()
    {
        if (nowNumber >= maxGenerateNumber)
            return;
        Reallocate();
        foreach (EnemyGeneratorRatio enemy in enemies)
        {
            int number = Random.Range(enemy.numberMin, enemy.numberMax + 1);
            for (int i = 0; i < number; i++)
            {
                // 找可以生的點
                List<Transform> gs = new List<Transform>();
                int min = maxGenerateNumber / generatePositions.Count;
                for (int j = 0;j < generatePositions.Count;j++)
                {
                    if (generatePositionNowNumber[generatePositions[j]] < min)
                        gs.Add(generatePositions[j]);
                }
                if (gs.Count == 0)
                    gs = new List<Transform>(generatePositions);
                Transform target = gs[Random.Range(0, gs.Count)];
                while (generatePositionNowNumber[target] >= generatePositionMaxNumber[target])
                {
                    gs.Remove(target);
                    target = gs[Random.Range(0, gs.Count)];
                }
                
                Enemy generatedEnemy = Instantiate(enemy.enemyPrefab, target.position, Quaternion.identity);
                if (area != null)
                    area.AddEnemy(generatedEnemy);
                nowNumber++;
                generatePositionNowNumber[target]++;
                generatedEnemy.OnDie += () => 
                {
                    nowNumber--;
                    generatePositionNowNumber[target]--;
                };
                if (nowNumber >= maxGenerateNumber)
                    return;
            }
        }
    }
}
