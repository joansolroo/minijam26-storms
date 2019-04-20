using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILightHP : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] LightSource light;
    [SerializeField] int bars = 8;

    float current;
    float max;

    private void Start()
    {
        UpdateBar();
    }
    // Update is called once per frame
    void Update()
    {
        if (light.getRange() != current)
        {
            UpdateBar();
        }
    }

    void UpdateBar()
    {
        current = light.getRange();
        max = light.getMaxRange();

        string b = "";
        float ratio = bars * current / max;
        for (int t = 0; t < ratio; ++t )
        {
            b += "|";
        }
        text.text = b;
    }
}
