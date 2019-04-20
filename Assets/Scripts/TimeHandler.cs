using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float dayDuration;
    [SerializeField] public static float dayTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time +=  Time.deltaTime / dayDuration;
        dayTime = time ;
        this.transform.eulerAngles = new Vector3(0, 0, dayTime * 360);
    }
}
