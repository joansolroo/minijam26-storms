using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : LightSource
{
    [SerializeField] float animationSpeed;
    [SerializeField] Light light;
    [SerializeField] AnimationCurve intensity;
    [SerializeField] Gradient color;
    [SerializeField] float strength = 1;
    float t;

    Vector3 lightPosition;

    [SerializeField] float rangeMax = 1;
    [SerializeField] float range = 1;
    [SerializeField] GameObject rangeRenderer;
    [SerializeField] ParticleSystem emission;


    public override Vector3 getPosition()
    {
        Vector3 pos = this.transform.position;
        pos.y = 0;
        return pos;
    }
    public override float getMaxRange()
    {
        return rangeMax;
    }

    public override float getRange()
    {
        return range;
    }

    private void Start()
    {
        lightPosition = light.transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            range += 2;
            range = Mathf.Min(rangeMax, range);
        }
        else
        {
            range -= RainHandler.intensity * Time.deltaTime * 0.1f;
            range = Mathf.Max(0, range); 
        }

        t += Time.deltaTime* animationSpeed;
        light.intensity = intensity.Evaluate(t % 1)* Mathf.Sqrt(strength);
        light.color = color.Evaluate(t % 1);
        light.range = strength * range*4;
        light.transform.localPosition = Vector3.MoveTowards(light.transform.localPosition, lightPosition + new Vector3(Random.Range(-0.1f * animationSpeed, 0.1f * animationSpeed), Random.Range(-0.1f * animationSpeed, 0.1f * animationSpeed), Random.Range(-0.1f * animationSpeed, 0.1f * animationSpeed)),Time.deltaTime);

        rangeRenderer.transform.localScale = Vector3.one * range;
        ParticleSystem.EmissionModule module = emission.emission;
        module.rateOverTime = light.range;
        module.rateOverDistance = light.range;

    }
}
