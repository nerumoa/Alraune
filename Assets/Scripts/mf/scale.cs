using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scale : MonoBehaviour
{
    [SerializeField] private Vector3 maxScale;

    private float power = 0.70588235f;
    private float timef;

    public double timeb;

    public void Start()
    {
        //timeb = GameObject.Find("Rythem Manager").GetComponent<RythemManager>().totalTime;

        //float timef = (float)timeb;
    }

    private void Update()
    {
        timeb = GameObject.Find("Rythem Manager").GetComponent<RythemManager>().totalTime;
        float timef = (float)(2 * Math.PI * timeb * power);
        transform.localScale = (Mathf.Sin(timef + 1) * 0.5f * maxScale);
    }
}
