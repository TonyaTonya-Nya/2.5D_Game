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
            int number = Random.Range(minNumber, maxNumber);
            for (int i = 0;i < number;i++)
            {
                float x = Random.Range(minX, maxX);
                float z = Random.Range(minZ, maxZ);
                GameObject hint =  Instantiate(HintPrefab, new Vector3(x, 0, z), Quaternion.Euler(0, 0, -90));
                GameObject meteorite = Instantiate(MeteoritePreafab, new Vector3(x, 100, z), Quaternion.Euler(-90, 0, 0));
                meteorite.GetComponent<Meteorite>().hintObject = hint;
            }
            yield return null;
        }
    }
}
