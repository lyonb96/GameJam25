using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxSunset;

    [SerializeField] private Gradient graddientNightToSunrise;
    [SerializeField] private Gradient graddientSunriseToDay;
    [SerializeField] private Gradient graddientDayToSunset;
    [SerializeField] private Gradient graddientSunsetToNight;

    [SerializeField] private Light globalLight;

    private int hours = 5;

    public int Hours
    { 
        get { return hours; } 
        set { hours = value; OnHoursChange(value); } 
    }

    private int days;

    public int Days
    { 
        get { return days; } 
        set { days = value; } 
    }

    private float tempSecond;
    float lerpSpeed = 10f;

    public void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     IncrementHour();
        // }
    }

    public void SetTime(int hour)
    {
        hours = hour;
        OnHoursChange(hour);
    }

    private void IncrementHour()
    {
        hours++;
        if (hours >= 24)
        {
            hours = 0;
            days++;
        }
        OnHoursChange(hours);
        globalLight.transform.Rotate(Vector3.up, (1f / 24f) * 360f, Space.World);
    }

    private IEnumerator LerpLightRotation(Transform lightTransform, Quaternion startRotation, Quaternion endRotation, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            lightTransform.rotation = Quaternion.Slerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        lightTransform.rotation = endRotation;
    }

    private void OnHoursChange(int value)
    {
        if (value == 6)
        {
            StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, lerpSpeed));
            StartCoroutine(LerpLight(graddientNightToSunrise, lerpSpeed));
        }
        else if (value == 8)
        {
            StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, lerpSpeed));
            StartCoroutine(LerpLight(graddientSunriseToDay, lerpSpeed));
        }
        else if (value == 18)
        {
            StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, lerpSpeed));
            StartCoroutine(LerpLight(graddientDayToSunset, lerpSpeed));
        }
        else if (value == 22)
        {
            StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, lerpSpeed));
            StartCoroutine(LerpLight(graddientSunsetToNight, lerpSpeed));
        }
    }

    private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
    {
        RenderSettings.skybox.SetTexture("_Texture1", a);
        RenderSettings.skybox.SetTexture("_Texture2", b);
        RenderSettings.skybox.SetFloat("_Blend", 0);
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            RenderSettings.skybox.SetFloat("_Blend", i / time);
            yield return null;
        }
        RenderSettings.skybox.SetTexture("_Texture1", b);
    }

    private IEnumerator LerpLight(Gradient lightGradient, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            globalLight.color = lightGradient.Evaluate(i / time);
            RenderSettings.fogColor = globalLight.color;
            yield return null;
        }
    }
}
