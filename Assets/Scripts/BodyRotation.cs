using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    [SerializeField] private CelestialTimeSettings timeSettings;

    [Header("Sidreal Rotation Time Settings")]
    [SerializeField] private float sidrealDays = 1f;
    [SerializeField] private float sidrealHours = 1f;
    [SerializeField] private float sidrealMinutes = 1f;
    [SerializeField] private float sidrealSeconds = 1f;

    float SiderealSeconds =>
        sidrealDays * 86400f +
        sidrealHours * 3600f +
        sidrealMinutes * 60f +
        sidrealSeconds;

    float RotationDegreesPerSecond => 360f / SiderealSeconds;


    private void Update()
    {
        if (!timeSettings) return;

        float degreesPerSecond = (360f / SiderealSeconds) * timeSettings.TotalTimeScale;

        transform.Rotate(degreesPerSecond * Time.deltaTime * Vector3.up);
    }

}
