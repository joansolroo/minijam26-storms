using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightSource : MonoBehaviour
{
    public static Dictionary<int, LightSource> lightSources = new Dictionary<int, LightSource>();
    static int MAXID = 0;
    int id;


    private void Awake()
    {
        id = ++MAXID;
        lightSources.Add(id,this);
    }

    public abstract float getRange();
    public abstract float getMaxRange();
    public abstract Vector3 getPosition();

    public static IEnumerable<LightSource> AllLightSources()
    {
        return lightSources.Values;
    }
}
