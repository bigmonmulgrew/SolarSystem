using UnityEngine;

[CreateAssetMenu(fileName = "CelestialTimeSettings", menuName = "Celestial/Time Settings")]
public class CelestialTimeSettings : ScriptableObject
{
    [Header("Time Scale Global Settings")]

    [Range(1, 365)]
    [Tooltip("Time multiplier for days.")]
    public float timeScaleDay = 1f;

    [Range(1, 24)]
    [Tooltip("Time multiplier for hours.")]
    public float timeScaleHours = 1f;

    [Range(1, 60)]
    [Tooltip("Time multiplier for minutes.")]
    public float timeScaleMinutes = 1f;

    [Range(1, 60)]
    [Tooltip("Time multiplier for seconds.")]
    public float timeScaleSeconds = 1f;

    public float TotalTimeScale =>
        timeScaleDay *
        timeScaleHours *
        timeScaleMinutes *
        timeScaleSeconds;



}
