using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteArea : MonoBehaviour
{
    public GameObject MeteoritePreafab;
    public GameObject HintPrefab;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public int minNumber;
    public int maxNumber;

    public float minTime;
    public float maxTime;

    public bool generateHint;
    public float generateY;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (true)
        {
            float time = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(time);
            int number = Random.Range(minNumber, maxNumber + 1);
            for (int i = 0;i < number;i++)
            {
                float x = Random.Range(minX, maxX + 1);
                float z = Random.Range(minZ, maxZ + 1);
                GameObject meteorite = Instantiate(MeteoritePreafab, new Vector3(x, generateY, z), Quaternion.Euler(-90, 0, 0));
                if (generateHint)
                {
                    GameObject hint = Instantiate(HintPrefab, new Vector3(x, 0.5f, z), Quaternion.Euler(-90, 0, 0));
                    if (meteorite.TryGetComponent<Meteorite>(out Meteorite m))
                        m.GetComponent<Meteorite>().hintObject = hint;
                }
            }
            yield return null;
        }
    }
}
