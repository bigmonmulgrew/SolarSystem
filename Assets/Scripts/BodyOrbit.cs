using UnityEngine;

public class BodyOrbit : Body
{
    [Header("Sidreal Orbit Time Settings")]
    [SerializeField] float sidrealDays = 1f;
    [SerializeField] float sidrealHours = 0f;
    [SerializeField] float sidrealMinutes = 0f;
    [SerializeField] float sidrealSeconds = 0f;

    [Header("Orbital Range Settings")]
    [SerializeField] float meanOrbitalRange = 1;

    GameObject child;
    GameObject childMarker;

    float SiderealSeconds =>
        sidrealDays * 86400f +
        sidrealHours * 3600f +
        sidrealMinutes * 60f +
        sidrealSeconds;

    float OrbitRange()
    {
        float r = meanOrbitalRange;

        switch (celestialSettings.orbitalScalemode)
        {
            case OrbitScaleMode.Linear:
                r /= celestialSettings.OrbitScale;
                break;

            case OrbitScaleMode.Sqrt:
                r = Mathf.Sqrt(r / celestialSettings.OrbitScale);
                break;

            //case OrbitScaleMode.Log10:
            //    float logR = Mathf.Log10(r / celestialSettings.LogReference);

            //    r = logR * celestialSettings.orbitalScale;
            //    break;
        }

        return r;
    }

    private void Awake()
    {
        CreateChildMarker();
        SetorbitalRange();
    }

    private void SetorbitalRange()
    {
        Vector3 newRange = childMarker.transform.position;
        newRange.x = OrbitRange(); 
        childMarker.transform.position = newRange;
    }

    private void CreateChildMarker()
    {
        // Creates a marker that rotates with the orbit.
        // Detatches the child so it rotates independently
        child = transform.GetChild(0).gameObject;
        childMarker = new GameObject("ChildMarker");
        childMarker.transform.position = child.transform.position;
        childMarker.transform.rotation = Quaternion.identity;
        childMarker.transform.localScale = child.transform.localScale;
        childMarker.transform.parent = child.transform.parent;
        child.transform.parent = null;
    }

    private void Update()
    {
        if (!celestialSettings) return;

        float degreesPerSecond = (360f / SiderealSeconds) * celestialSettings.TotalTimeScale;

        transform.Rotate(degreesPerSecond * Time.deltaTime * Vector3.up);

        child.transform.position = childMarker.transform.position;
        
    }
}
