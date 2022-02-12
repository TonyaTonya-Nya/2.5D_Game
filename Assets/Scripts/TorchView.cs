using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct TorchEffectParameter
{
    public float alpha;
    public float space;
}

public class TorchView : MonoBehaviour
{
    public Image mask;

    public List<TorchEffectParameter> effects;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (true)
        {
            foreach (TorchEffectParameter param in effects)
                yield return RunEffect(param.alpha, param.space);
        }
    }

    private IEnumerator RunEffect(float alphaTraget, float space)
    {
        if (mask.color.a < alphaTraget)
        {
            while (mask.color.a < alphaTraget)
            {
                mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, mask.color.a + space);
                yield return null;
            }
            mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, alphaTraget);
        }
        else if (mask.color.a > alphaTraget)
        {
            while (mask.color.a > alphaTraget)
            {
                mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, mask.color.a - space);
                yield return null;
            }
            mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, alphaTraget);
        }
    }
}
