using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainHandler : MonoBehaviour
{
    [SerializeField] AudioClip[] layers;
    [SerializeField] AudioClip thunder;

    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] ParticleSystem raindropParticles;
    [SerializeField] Light lightning;
    [SerializeField] AudioSource lightningAudioControl;

    List<AudioSource> layerAudioControls = new List<AudioSource>();
    [SerializeField] public static float intensity = 0;
    [SerializeField] [Range(0f, 1f)] float rainVolume = 0;
    [SerializeField] [Range(0f, 1f)] float stormDistance = 0;

    [SerializeField] AnimationCurve intensityProgression;

    [SerializeField] Transform sky;
    // Start is called before the first frame update
    void Start()
    {
        foreach(AudioClip layer in layers)
        {
            GameObject go = new GameObject();
            go.transform.parent = this.transform;
            go.transform.localPosition = Vector3.zero;
            AudioSource layerControl = go.AddComponent<AudioSource>();
            layerControl.clip = layer;
            layerControl.loop = true;
            layerControl.Play();
            layerAudioControls.Add(layerControl);
        }
    }
    public bool doLightning = false;
    // Update is called once per frame
    void Update()
    {
        intensity = intensityProgression.Evaluate(TimeHandler.dayTime);
        for (int idx = 0; idx < layerAudioControls.Count; ++idx)
        {
            layerAudioControls[idx].volume = Mathf.Lerp(0, 1, Mathf.Pow(intensity * (layerAudioControls.Count-idx),8))* rainVolume;
        }
        rainParticles.emissionRate = Mathf.Lerp(2, 1000, intensity);
        raindropParticles.emissionRate = Mathf.Lerp(2, 1000, intensity);
        doLightning |= intensity > 0.4f && Random.value < 0.25f*Time.deltaTime*intensity;
        doLightning |= Input.GetKeyDown(KeyCode.Space);
        if (doLightning)
        {
            StartCoroutine(DoLightning());
            doLightning = false;
        }
        sky.transform.localEulerAngles = new Vector3(TimeHandler.dayTime * 360+90, 80,0);
    }

    bool doingLighting = false;
    IEnumerator DoLightning()
    {
        if (!doingLighting)
        {
            doingLighting = true;
            lightning.intensity = 3;
            yield return new WaitForSeconds(stormDistance * 0.25f);
            //lightningAudioControl.clip = thunder;
            lightningAudioControl.pitch = Random.Range(0.8f, 1.05f);
            lightningAudioControl.PlayOneShot(thunder);
            yield return new WaitForSeconds((1 - stormDistance) * 0.25f);
            lightning.intensity = 0;
            yield return new WaitForSeconds(3);
            doingLighting = false;
        }
    }
}
