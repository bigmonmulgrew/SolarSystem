using UnityEngine;

public class BodyRotation : Body
{
    [Header("Sidreal Rotation Time Settings")]
    [SerializeField] float sidrealDays = 1f;
    [SerializeField] float sidrealHours = 0f;
    [SerializeField] float sidrealMinutes = 0f;
    [SerializeField] float sidrealSeconds = 0f;

    float SiderealSeconds =>
        sidrealDays * 86400f +
        sidrealHours * 3600f +
        sidrealMinutes * 60f +
        sidrealSeconds;

    private void Update()
    {
        if (!celestialSettings) return;

        float degreesPerSecond = (360f / SiderealSeconds) * celestialSettings.TotalTimeScale;

        transform.Rotate(degreesPerSecond * Time.deltaTime * Vector3.up);
    }

}
