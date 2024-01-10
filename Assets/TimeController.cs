using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeController : MonoBehaviour {
    [SerializeField, Tooltip("The amount of minutes a day should last."), Header("Initial Configuration")]
    private float minutesInDay;

    [SerializeField, Range(0, 24)] private float startHour = 12;

    [Header("UI Elements")]
    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float sunriseHour;
    [SerializeField]
    private float sunsetHour;

    // Timespans
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    private DateTime currentTime;

    // Sunlight
    private int MINUTES_IN_DAY = 24 * 60;
    // Start is called before the first frame update
    void Start() {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
    }

    // Update is called once per frame
    void Update() {
        UpdateTimeOfDay();
        RotateSun();
    }
    private void UpdateTimeOfDay() {

        currentTime = currentTime.AddSeconds(Time.deltaTime * MINUTES_IN_DAY / minutesInDay);
        if (timeText != null) {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    private TimeSpan CalculateTimeDifference(TimeSpan from, TimeSpan to) {
        TimeSpan diff = from - to;

        if (diff.TotalSeconds < 0) {
            diff += TimeSpan.FromDays(1);
        }
        return diff;
    }
    private void RotateSun() {
        float currentHour = (float)currentTime.TimeOfDay.TotalHours;
        // float sunRotation = Mathf.Lerp(-90, 270, currentHour / 24);

        float sunRotation = ((currentHour) / 24) * 360;
        var rotation = sunLight.transform.rotation;
        rotation = Quaternion.Euler(sunRotation - 90 , 0,0);
        sunLight.transform.rotation = rotation;
    }
}
