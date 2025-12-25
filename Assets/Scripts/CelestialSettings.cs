using UnityEngine;

[CreateAssetMenu(fileName = "CelestialSettings", menuName = "Celestial/Settings")]
public class CelestialSettings : ScriptableObject
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

    [Header("Scale Settings")]
    [Range(1, 696340000f)]
    [Tooltip("Scaling factor for all sizes to unity scale. Max value is one sun radius")]
    public float sizeScale = 696340000f;
    [Tooltip("Scaling factor for orbital distance. Increase value for closer orbits.")]
    public float orbitalScale = 1f;
    public OrbitScaleMode orbitalScalemode = OrbitScaleMode.Linear;
    //[Range(696340000f, 149597870700f)]
    //public float logReference = 149597870700f;
    
    public float TotalTimeScale =>
        timeScaleDay *
        timeScaleHours *
        timeScaleMinutes *
        timeScaleSeconds;

    public float OrbitScale => sizeScale * orbitalScale;
    //public float LogReference => logReference;

}
public enum OrbitScaleMode
{
    Linear,
    Sqrt,
    //Log10
}